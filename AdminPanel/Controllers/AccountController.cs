using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPanel.Const;
using AdminPanel.Models;
using AdminPanel.Services.Utils;
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

        public AccountController(ApplicationContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
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

            var user = _db.tblUser
                .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user);
                return Json(Url.Action("Index", "Admin"));
            }
            return InvokeError("Incorrect username or password. Please try again.");
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