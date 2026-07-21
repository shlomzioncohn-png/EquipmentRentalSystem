using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IItemRepository:IRepository<Item>
    {
        Task<IEnumerable<Item>> GetByBusinessIdAsync(Guid businessId);
    }
}
