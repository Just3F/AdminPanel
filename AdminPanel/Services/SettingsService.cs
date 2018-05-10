using System;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels.Email;
using AdminPanel.ViewModels.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Services
{
    public class SettingsService : ServiceBase
    {
        public SettingsService(ApplicationContext db, IMapper mapper, IViewRenderService viewRenderService)
        {
            _db = db;
            _mapper = mapper;
            _viewRenderService = viewRenderService;
        }

        public SettingsPageViewModel GetGeneralSettings()
        {
            var generalSettings = _db.vlGeneralSettings.Select(x => new SettingsPageViewModel
            {
                IsRequiredEmailVerification = x.IsRequiredEmailVerification,
                IsRequiredPhoneVerification = x.IsRequiredPhoneVerification
            }).FirstOrDefault();
            return generalSettings;
        }

        public async Task EditGeneralSettings(SettingsPageViewModel settings)
        {
            var generalSettings = _db.vlGeneralSettings.FirstOrDefault();
            generalSettings.IsRequiredPhoneVerification = settings.IsRequiredPhoneVerification;
            generalSettings.IsRequiredEmailVerification = settings.IsRequiredEmailVerification;
            _db.SaveChanges();

            EmailService emailService = new EmailService();

            await emailService.SendEmailAsync(new EmailModel
            {
                Subject = "test subject",
                EmailTo = "just3f@yandex.ru",
                Body = await _viewRenderService.RenderToStringAsync("Templates/Email/EmailTemplate", null)
            });
        }
    }
}
