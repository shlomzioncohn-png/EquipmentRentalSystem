using Core.Models;
using Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBusinessService
    {
        Task<BusinessResource?> GetBusinessByIdAsync(Guid id);

        Task<BusinessResource?> CreateBusinessAsync(BusinessResource businessResource);
        

        Task<BusinessResource?> UpdateBusinessAsync(Guid id, BusinessResource business);

        Task<IEnumerable<BusinessResource>?> GetAllBusinessesAsync();
        Task<bool> DeleteBusinessAsync(Guid id);

        Task<IEnumerable<BusinessResource>> GetByUserIdAsync(Guid userId);


    }
}
