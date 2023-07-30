using EnglishAI.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.Interfaces
{
    public interface IRepository<TModel> 
        where TModel : ApplicationEntityBase
    {
        Task<IList<TModel>> GetAllAsync(CancellationToken cancellationToken);

        Task<TModel?> GetAsync(string id, CancellationToken cancellationToken);

        Task AddAsync(TModel model, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<TModel> models, CancellationToken cancellationToken);

        Task ReplaceAsync(TModel model, CancellationToken cancellationToken);

        Task RemoveAsync(string id, CancellationToken cancellationToken);
    }
}
