using Core.Models;
using Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IItemService
    {
        Task<ItemResource?> GetItemByIdAsync(Guid id);

        Task<ItemResource?> CreateItemAsync(ItemResource item);


        Task<ItemResource?> UpdateItemAsync(Guid id, ItemResource item);

        Task<IEnumerable<ItemResource>?> GetAllItemsAsync();
        Task<bool> DeleteItemAsync(Guid id);

        Task<IEnumerable<ItemResource>> GetByBusineIdAsync(Guid businessId);
    }
}
