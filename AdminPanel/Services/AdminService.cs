using System.Collections.Generic;
using System.Linq;
using AdminPanel.Models;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels;
using AdminPanel.ViewModels.Users;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            return _mapper.Map<List<UserViewModel>>(_db.tblUser.Include(x => x.UserVerification));
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

        public CategoriesModel GetCategories()
        {
            var categoriesModel = new CategoriesModel();
            categoriesModel.Categories.AddRange(_db.tblCategory.Select(x => new CategoryViewModel
            {
                Name = x.Name,
                Description = x.Description,
                PKID = x.PKID
            }).ToList());

            return categoriesModel;
        }

        public PostsModel GetPosts()
        {
            var categoriesModel = new PostsModel();
            categoriesModel.PostViewModels.AddRange(_db.tblPost.Select(x => new PostViewModel
            {
                Title = x.Title,
                Description = x.Description,
                PKID = x.PKID,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId
            }).ToList());

            return categoriesModel;
        }

        public CategoryViewModel GetCategory(long id)
        {
            return _db.tblCategory.Where(x => x.PKID == id).Select(x => new CategoryViewModel
            {
                Name = x.Name,
                Description = x.Description,
                PKID = x.PKID
            }).FirstOrDefault();
        }

        public CategoryViewModel ManageCategory(CategoryViewModel item)
        {
            if (item.PKID == 0)
            {
                var category = new tblCategory
                {
                    Description = item.Description,
                    Name = item.Name
                };
                _db.tblCategory.Add(category);
                _db.SaveChanges();
                item.PKID = category.PKID;
            }
            else
            {
                var category = _db.tblCategory.FirstOrDefault(x => x.PKID == item.PKID);
                category.Description = item.Description;
                category.Name = item.Name;
                _db.SaveChanges();
            }

            return item;
        }

        public PostViewModel GetPost(long id)
        {
            return _db.tblPost.Where(x => x.PKID == id).Select(x => new PostViewModel
            {
                Title = x.Title,
                Description = x.Description,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                PKID = x.PKID
            }).FirstOrDefault();
        }

        public PostViewModel ManagePost(PostViewModel item)
        {
            if (item.PKID == 0)
            {
                var post = new tblPost
                {
                    Description = item.Description,
                    Title = item.Title,
                    CategoryId = item.CategoryId
                };
                _db.tblPost.Add(post);
                _db.SaveChanges();
                item.PKID = post.PKID;
            }
            else
            {
                var post = _db.tblPost.FirstOrDefault(x => x.PKID == item.PKID);
                post.Description = item.Description;
                post.Title = item.Title;
                post.CategoryId = item.CategoryId;
                _db.SaveChanges();
            }

            return item;
        }
    }
}
