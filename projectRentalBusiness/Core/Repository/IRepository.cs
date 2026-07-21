using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>?> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<T> AddAsync(T obj);

        Task<T?> UpdateAsync(Guid id,T obj);

        Task<int> DeleteAsync(Guid id);
    }
}
