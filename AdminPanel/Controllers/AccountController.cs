using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPanel.Const;
using AdminPanel.Models;
using AdminPanel.Services.Utils;
using AdminPanel.ViewModels.Email;
using AdminPanel.ViewModels.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Controllers
{
    public class AccountController : ControllerBase
    {
        private ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly IViewRenderService _viewRenderService;

        public AccountController(ApplicationContext context, IMapper mapper, IViewRenderService viewRenderService)
        {
            _db = context;
            _mapper = mapper;
            _viewRenderService = viewRenderService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return InvokeError("A email and password must be entered!");

            var user = await _db.tblUser.Include(x=>x.UserVerification)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                var generalSettings = _db.vlGeneralSettings.FirstOrDefault();

                if (!user.UserVerification.EmailActivated && generalSettings.IsRequiredEmailVerification)
                    return InvokeError("Email address is not verified.");

                if (!user.UserVerification.PhoneActivated && generalSettings.IsRequiredPhoneVerification)
                    return InvokeError("Phone number is not verified.");

                await Authenticate(user);
                return Json(Url.Action("Profile", "Admin"));
            }
            return InvokeError("Incorrect username or password. Please try again.");
        }

        [HttpPost]
        public async Task<JsonResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join("<br/>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return InvokeError(message);
            }

            if (_db.tblUser.Any(x => x.Email.Equals(model.Email)))
            {
                return InvokeError("Email address already exists.");
            }

            var emailCode = Guid.NewGuid().ToString();
            var newUser = new tblUser
            {
                Email = model.Email,
                Password = model.Password,
                Role = Role.User,
                UserVerification = new tblUserVerification
                {
                    SentEmailCodeTime = DateTime.Now,
                    EmailCode = emailCode
                }
            };
            await _db.tblUser.AddAsync(newUser);
            await _db.SaveChangesAsync();

            var linkOnVerify = EmailHelper._SiteURL + Url.Action("EmailVerification", "Account", new { userId = newUser.PKID, code = emailCode });
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(new EmailModel
            {
                Subject = "Confirm Registration",
                EmailTo = newUser.Email,
                Body = await _viewRenderService.RenderToStringAsync("Templates/Email/EmailConfirmTemplate", new EmailConfirmTemplateModel
                {
                    Email = newUser.Email,
                    SiteName = "Admin Panel",
                    LinkOnVerify = linkOnVerify
                }),
                //Body = "<b>Hi " + newUser.Email + " </b><br/> Welcome to [SiteName]! <br/>" +
                //       "<a href='" + linkOnVerify + "'>Confirm email address</a>"
            });
            return SuccessJsonResult();
        }

        public async Task<IActionResult> EmailVerification(long userId, string code)
        {
            var user = await _db.tblUser.Include(x => x.UserVerification).FirstOrDefaultAsync(x => x.PKID == userId);

            if (user.UserVerification.EmailActivated)
            {
                ViewBag.Response = "Your email has been already verified";
                return View();
            }

            if (user.UserVerification.EmailCode.Equals(code))
            {
                user.UserVerification.EmailActivated = true;
                await _db.SaveChangesAsync();
                ViewBag.Response = "Congratulations! You have verified your email.";
            }
            else
            {
                ViewBag.Response = "Error! Check your code.";
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginOld(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _db.tblUser
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower() && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        private async Task Authenticate(tblUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.PKID.ToString()),
                new Claim("FirstName", user.FirstName ?? ""),
                new Claim("LastName", user.LastName ?? ""),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Account");
        }
    }
}