using AutoMapper;
using EnglishAI.Application.Interfaces;
using EnglishAI.Application.Models.DB;
using EnglishAI.Infrastructure.DBRepositories.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.DBRepositories
{
    public class IrregularVerbRepository : RepositoryBase<IrregularVerb, IrregularVerbEntity>, IIrregularVerbRepository
    {
        public IrregularVerbRepository(IMongoDatabase database, IMapper mapper) : base(database, mapper)
        {
        }
    }
}
