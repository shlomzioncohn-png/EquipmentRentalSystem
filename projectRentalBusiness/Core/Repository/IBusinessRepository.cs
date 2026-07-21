using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IBusinessRepository:IRepository<Business>
    {
        Task<IEnumerable<Business>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Business>> GetBusinessesByCityAsync(string city);
    }
}
