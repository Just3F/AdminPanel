using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Services;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels.Email;
using AdminPanel.ViewModels.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class SettingsController : ControllerBase
    {
        private readonly SettingsService _settingsService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IMapper _mapper;

        public SettingsController(ApplicationContext dbContext, IMapper mapper, IViewRenderService viewRenderService)
        {
            _settingsService = new SettingsService(dbContext, mapper, viewRenderService);
        }

        public IActionResult Index()
        {
            var generalSettings = _settingsService.GetGeneralSettings();
            return View(generalSettings);
        }

        public IActionResult Test()
        {
            return View("Templates/Email/EmailTemplate", new EmailTemplateModel());
        }

        [HttpPost]
        public async Task<IActionResult> EditGeneralSettings(SettingsPageViewModel settings)
        {
            await _settingsService.EditGeneralSettings(settings);
            return RedirectToAction("Index");
        }
    }
}