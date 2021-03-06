﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels;
using AutoMapper;

namespace AdminPanel.Services
{
    public class HomeService : ServiceBase, IHomeService
    {
        public HomeService(ApplicationContext db, IMapper mapper, IViewRenderService viewRenderService)
        {
            _db = db;
            _mapper = mapper;
            _viewRenderService = viewRenderService;
        }

        public List<CategoryViewModel> GetCategories()
        {
            return _db.tblCategory.Select(x => new CategoryViewModel
            {
                Description = x.Description,
                Name = x.Name,
                PKID = x.PKID
            }).ToList();
        }

        public List<MainCategoryViewModel> GetMainCategories()
        {
            return _db.tblMainCategory.Select(x => new MainCategoryViewModel()
            {
                Order = x.Order,
                Name = x.Name,
                PKID = x.PKID,
                CategoryViewModels = x.Categories.Select(z => new CategoryViewModel
                {
                    Description = z.Description,
                    Name = z.Name
                }).ToList()
            }).OrderBy(x => x.Order).ToList();
        }

        public List<PostViewModel> GetPost(long id)
        {
            return _db.tblPost.Where(x => x.CategoryId == id).Select(x => new PostViewModel
            {
                CategoryName = x.Category.Name,
                Description = x.Description,
                Title = x.Title,
                PKID = x.PKID,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public long? GetMainPost()
        {
            return _db.tblPost.FirstOrDefault(x => x.IsMain)?.PKID;
        }
    }
}
