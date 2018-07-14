using AdminPanel.Models;
using AdminPanel.Services;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels;
using AdminPanel.ViewModels.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(ApplicationContext dbContext, IMapper mapper)
        {
            _adminService = new AdminService(dbContext, mapper);
        }

        public IActionResult Categories()
        {
            CategoriesModel models = _adminService.GetCategories();
            return View(models);
        }

        public IActionResult ManagePost(long? id)
        {
            ManagePostViewModel model = new ManagePostViewModel();
            if (id != null)
                model.PostViewModel = _adminService.GetPost(id.Value);

            model.Categories = _adminService.GetCategories().Categories;

            return View(model);
        }

        [HttpPost]
        public IActionResult ManagePost(PostViewModel item)
        {
            item = _adminService.ManagePost(item);

            return RedirectToAction("ManagePost", new {id = item.PKID});
        }

        public IActionResult ManageCategory(long? id)
        {
            CategoryViewModel category = new CategoryViewModel();
            if (id != null)
                category = _adminService.GetCategory(id.Value);

            return View(category);
        }

        [HttpPost]
        public IActionResult ManageCategory(CategoryViewModel item)
        {
            item = _adminService.ManageCategory(item);
            return RedirectToAction("ManageCategory", new {id = item.PKID});
        }

        public IActionResult Posts()
        {
            PostsModel models = _adminService.GetPosts();
            return View(models);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            var user = _adminService.GetUserById(AppUser.GetId());
            return View(user);
        }

        public IActionResult Users()
        {
            var users = _adminService.GetUsers();
            return View(users);
        }
        public IActionResult UserManage(long id)
        {
            var user = _adminService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserViewModel user)
        {
            var userId = _adminService.EditUser(user);
            return RedirectToAction("UserManage", new { id = userId });
        }
    }
}