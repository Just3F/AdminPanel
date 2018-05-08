using System.Collections.Generic;
using System.Linq;
using AdminPanel.Models;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels.Users;
using AutoMapper;

namespace AdminPanel.Services
{
    public class AdminService : ServiceBase
    {
        private readonly AdminService _adminService;

        public AdminService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public UserViewModel GetUserById(long id)
        {
            var user = _mapper.Map<UserViewModel>(_db.tblUser.FirstOrDefault(x => x.PKID == id));
            return user;
        }

        public List<UserViewModel> GetUsers()
        {
            return _mapper.Map<List<UserViewModel>>(_db.tblUser);
        }

        public long EditUser(UserViewModel user)
        {
            var userId = user.PKID;
            if (user.PKID == 0)
            {
                var newUser = new tblUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Password = user.Password,
                    Role = user.Role
                };
                _db.tblUser.Add(newUser);
                _db.SaveChanges();
                userId = newUser.PKID;
            }
            else
            {
                var existUser = _db.tblUser.FirstOrDefault(x => x.PKID == user.PKID);
                existUser.IsActive = user.IsActive;
                existUser.Email = user.Email;
                existUser.Password = user.Password;
                existUser.FirstName = user.FirstName;
                existUser.LastName = user.LastName;
                existUser.Role = user.Role;
                _db.SaveChanges();
            }

            return userId;
        }
    }
}
