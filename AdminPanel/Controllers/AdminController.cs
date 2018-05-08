using AdminPanel.Const;
using AdminPanel.Models;
using AdminPanel.Services;
using AdminPanel.Services.Utils;
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
            return RedirectToAction("UserManage", new {id = userId });
        }
    }
}