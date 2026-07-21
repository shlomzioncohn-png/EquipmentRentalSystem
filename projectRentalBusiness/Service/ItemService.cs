using AutoMapper;
using Core.Models;
using Core.Repository;
using Core.Resources;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ItemService: Core.Services.IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _businessRepository;
        public ItemService(IItemRepository itemTypeRepository, IMapper mapper, IBusinessRepository businessRepository)
        {
            _itemRepository = itemTypeRepository;
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var existingVehicleType = await _itemRepository.GetByIdAsync(id);
            if (existingVehicleType == null)
                return false;
            var result = await _itemRepository.DeleteAsync(id);
            return result > 0;
        }

        public async Task<IEnumerable<ItemResource>?> GetAllItemsAsync()
        {
            var itemList = await _itemRepository.GetAllAsync();
            if (itemList == null || !itemList.Any())
            {
                return null;
            }
            return _mapper.Map<IEnumerable<ItemResource>>(itemList);
        }

        public async Task<ItemResource?> UpdateItemAsync(Guid id, ItemResource item)
        {
            if (item == null)
                return null;

            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
                return null;
            _mapper.Map(item, existingItem);
            var updatedItem = await _itemRepository.UpdateAsync(id, existingItem);
            return _mapper.Map<ItemResource>(updatedItem);
        }

        public async Task<ItemResource?> CreateItemAsync(ItemResource itemResource)
        {
            var business = await _businessRepository.GetByIdAsync(itemResource.BusinessId);
            if (business == null)
                return null;
            var item = _mapper.Map<Item>(itemResource);
            var createdItem = await _itemRepository.AddAsync(item);
            if (createdItem == null)
                return null;
            return _mapper.Map<ItemResource>(createdItem);
        }

        public async Task<ItemResource?> GetItemByIdAsync(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null)
                return null;
            return _mapper.Map<ItemResource>(item);

        }

        public async Task<IEnumerable<ItemResource>> GetByBusineIdAsync(Guid businessId)
        {
            var items = await _itemRepository.GetByBusinessIdAsync(businessId);

            return _mapper.Map<IEnumerable<Item>, IEnumerable<ItemResource>>(items);
        }
    }
}
