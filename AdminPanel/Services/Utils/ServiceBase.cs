using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models;
using AutoMapper;

namespace AdminPanel.Services.Utils
{
    public class ServiceBase
    {
        protected ApplicationContext _db { get; set; }
        protected IViewRenderService _viewRenderService { get; set; }
        protected IMapper _mapper { get; set; }
    }
}
