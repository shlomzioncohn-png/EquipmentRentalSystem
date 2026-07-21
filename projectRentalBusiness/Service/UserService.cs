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
    public class UserService:IUserService
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper,IBusinessRepository businessRepository,IEmailService emailService)
        {
            _userRepository=userRepository;
            _mapper = mapper;
            _businessRepository=businessRepository;
            _emailService=emailService;
        }
        
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null) return false;

            var businesses = await _businessRepository.GetByUserIdAsync(id);
            if (businesses != null && businesses.Any())
            {
                return false;
            }

            var result = await _userRepository.DeleteAsync(id);
            return result > 0;
        }

        public async Task<IEnumerable<UserResource>?> GetAllUsersAsync()
        {
            var userList = await _userRepository.GetAllAsync();
            if (userList == null || !userList.Any())
            {
                return null;
            }
            return _mapper.Map<IEnumerable<UserResource>>(userList);
        }

        public async Task<UserResource?> UpdateUserAsync(Guid id, UserResource user)
        {
            if (user == null)
                return null;

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return null;
            _mapper.Map(user, existingUser);
            var updatedUser = await _userRepository.UpdateAsync(id, existingUser);
            await _emailService.SendEmailAsync(user.Email, "אישור עדכון פרופיל !!😊😍", "הפרופיל שלך עודכן בהצלחה" + user.Password, user.Name);

            return _mapper.Map<UserResource>(updatedUser);
        }

        public async Task<UserResource?> CreateUserAsync(UserCreateResource userCreateResource)
        {
            var createdUser = _mapper.Map<UserCreateResource, User>(userCreateResource);

            var existingUser = await _userRepository.GetByEmailAsync(createdUser.Email);

            if (existingUser != null)
            {
                return null;
            }

            var user = await _userRepository.AddAsync(createdUser);
            if (user == null)
                return null;
           await _emailService.SendEmailAsync(user.Email, "אישור הרשמה לאתר שלנו!!😊😍","תודה שנרשמת אלינו הסיסמא שלך היא-"+user.Password,user.Name);

            return _mapper.Map<UserResource>(user);
        }

        public async Task<UserResource?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;
            return _mapper.Map<UserResource>(user);
        }

        public async Task<UserResource?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.Password != password)
            {
                return null;
            }
            return _mapper.Map<UserResource>(user);
        }



    }
}
