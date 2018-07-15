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
            List<PostViewModel> post = new List<PostViewModel>();
            var idOfMainPost = _homeService.GetMainPost();
            if (idOfMainPost != null)
            {
                post = _homeService.GetPost(idOfMainPost.Value);
            }
            return View("Post", post);
        }

        public IActionResult Post(long? id)
        {
            List<PostViewModel> post = new List<PostViewModel>();
            if (id != null)
            {
                post = _homeService.GetPost(id.Value);
            }
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
