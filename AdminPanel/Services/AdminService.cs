using AdminPanel.Models;
using AdminPanel.Services.Utils;
using AutoMapper;

namespace AdminPanel.Services
{
    public class AdminService: ServiceBase
    {
        private readonly AdminService _adminService;

        public AdminService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    }
}
