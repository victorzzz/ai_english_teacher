using AutoMapper;
using EnglishAI.Application.Interfaces;
using EnglishAI.Application.Models;
using EnglishAI.Infrastructure.DBRepositories.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.DBRepositories
{
    public class RepositoryBase<TApplicationModel, TDBModel> : IRepository<TApplicationModel>
        where TApplicationModel : ApplicationEntityBase
        where TDBModel : EntityBase
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public IMongoCollection<TDBModel> Collection { get; init; }

        private static string GetCollectionName()
        {
            var collectionName = typeof(TDBModel).Name;
            if (collectionName.EndsWith("Entity"))
            {
                collectionName = collectionName[..^6];
            }
            return collectionName;
        }


        public RepositoryBase(IMongoDatabase database, IMapper mapper)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Collection = _database.GetCollection<TDBModel>(GetCollectionName());
        }

        Task IRepository<TApplicationModel>.AddAsync(TApplicationModel model, CancellationToken cancellationToken)
        {
            var dbModel = _mapper.Map<TDBModel>(model);
            return Collection.InsertOneAsync(dbModel, null, cancellationToken);
        }

        Task IRepository<TApplicationModel>.AddRangeAsync(IEnumerable<TApplicationModel> models, CancellationToken cancellationToken)
        {
            var dbModels = _mapper.Map<IEnumerable<TDBModel>>(models);
            return Collection.InsertManyAsync(dbModels, null, cancellationToken);
        }

        async Task<IList<TApplicationModel>> IRepository<TApplicationModel>.GetAllAsync(CancellationToken cancellationToken)
        {
            var dbModels = await Collection.Find(new BsonDocument()).ToListAsync(cancellationToken);
            return _mapper.Map<IList<TApplicationModel>>(dbModels);
        }

        async Task<TApplicationModel?> IRepository<TApplicationModel>.GetAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<TDBModel>.Filter.Eq(doc => doc.Id, id);
            var dbModel = await Collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

            return dbModel == null ? default : _mapper.Map<TApplicationModel>(dbModel);
        }

        Task IRepository<TApplicationModel>.RemoveAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<TDBModel>.Filter.Eq(doc => doc.Id, id);
            return Collection.DeleteOneAsync(filter, cancellationToken);
        }

        Task IRepository<TApplicationModel>.ReplaceAsync(TApplicationModel model, CancellationToken cancellationToken)
        {
            var dbModel = _mapper.Map<TDBModel>(model);

            if (dbModel.Id == null)
            {
                throw new InvalidOperationException("Update failed. Model doesn't have an 'Id' property.");
            }

            var filter = Builders<TDBModel>.Filter.Eq(doc => doc.Id, dbModel.Id);
            return Collection.ReplaceOneAsync(filter, dbModel, new ReplaceOptions(), cancellationToken);
        }
    }
}
