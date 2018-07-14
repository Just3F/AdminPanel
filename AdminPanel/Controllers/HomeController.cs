using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.Models;
using AdminPanel.Services;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels;
using AutoMapper;

namespace AdminPanel.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly HomeService _homeService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IMapper _mapper;

        public HomeController(ApplicationContext dbContext, IMapper mapper, IViewRenderService viewRenderService)
        {
            _homeService = new HomeService(dbContext, mapper, viewRenderService);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post(long id)
        {
            var post = _homeService.GetPost(id);
            return View(post);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
