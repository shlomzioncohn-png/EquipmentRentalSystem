using Core.Models;
using Core.Repository;
using Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        Task<UserResource?> GetUserByIdAsync(Guid id);

        Task<UserResource?> CreateUserAsync(UserCreateResource userCreateResource);


        Task<UserResource?> UpdateUserAsync(Guid id, UserResource user);

        Task<IEnumerable<UserResource>?> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(Guid id);

        Task<UserResource?> LoginAsync(string email, string password);

    }
}
