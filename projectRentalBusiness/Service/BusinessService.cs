using AutoMapper;
using Core.Mapping;
using Core.Models;
using Core.Repository;
using Core.Resources;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BusinessService:IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository; 
        private readonly IMapper _mapper;
        public BusinessService(IBusinessRepository businessRepository,IMapper mapper,IUserRepository userRepository, IItemRepository iitemRepository)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _itemRepository = iitemRepository;

        }

        public async Task<bool> DeleteBusinessAsync(Guid id)
        {
            var existingBusiness = await _businessRepository.GetByIdAsync(id);
            if (existingBusiness == null) return false;

            var items = await _itemRepository.GetByBusinessIdAsync(id);

            if (items != null && items.Any())
            {
                return false;
            }

            var result = await _businessRepository.DeleteAsync(id);
            return result > 0;
        }
        

        public async Task<IEnumerable<BusinessResource>?> GetAllBusinessesAsync()
        {
           var businessList=await _businessRepository.GetAllAsync();
            if(businessList == null || !businessList.Any())
            {
                return null;
            }
            return _mapper.Map<IEnumerable<BusinessResource>>(businessList);
        }

        public async Task<BusinessResource?> UpdateBusinessAsync(Guid id, BusinessResource business)
        {
            if (business == null)
                return null;

            var existingBusiness =await _businessRepository.GetByIdAsync(id);
            if(existingBusiness == null)
                return null;
            _mapper.Map(business, existingBusiness);
            var updatedBusiness =await _businessRepository.UpdateAsync(id, existingBusiness);
            return _mapper.Map<BusinessResource>(updatedBusiness);
        }

        public async Task<BusinessResource?> CreateBusinessAsync(BusinessResource businessResource)
        {
            if (businessResource == null ||
                string.IsNullOrWhiteSpace(businessResource.Name))
            {
                return null; 
            }

            var userExists = await _userRepository.GetByIdAsync(businessResource.UserId);
            if (userExists == null)
            {
                return null;
            }

            var business = _mapper.Map<Business>(businessResource);
            
            var createdBusiness = await _businessRepository.AddAsync(business);

            if (createdBusiness == null)
                return null;

            return _mapper.Map<BusinessResource>(createdBusiness);
        }

        public async Task<BusinessResource?> GetBusinessByIdAsync(Guid id)
        {
            var business = await _businessRepository.GetByIdAsync(id);
            if(business == null)
                return null;
            return _mapper.Map<BusinessResource>(business);
        }

        

        public async Task<IEnumerable<BusinessResource>> GetByUserIdAsync(Guid userId)
        {
            var businesses = await _businessRepository.GetByUserIdAsync(userId);

            if (businesses == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<Business>, IEnumerable<BusinessResource>>(businesses);
        }

      
    }
}
