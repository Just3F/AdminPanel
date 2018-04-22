using AdminPanel.Models;
using AdminPanel.Services;
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
    }
}