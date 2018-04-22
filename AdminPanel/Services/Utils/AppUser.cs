using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AdminPanel.Services.Utils
{
    public class AppUser
    {
        public static long GetId()
        {
            var id = Convert.ToInt64(RequestContextManager.Instance.CurrentContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return id;
        }

        public static string GetCurrentUrl()
        {
            var host = RequestContextManager.Instance.CurrentContext.Request.Host.Host;
            var port = RequestContextManager.Instance.CurrentContext.Request.Host.Port;

            return "http://" + host + ":" + port;
        }

        public static string GetUserName()
        {
            var userName = RequestContextManager.Instance.CurrentContext.User.FindFirst("FirstName")?.Value;
            return userName;
        }

        public static string GetUserEmail()
        {
            var role = RequestContextManager.Instance?.CurrentContext?.User
                           .FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value ?? "";
            return role;
        }

        public static string GetFullUserName()
        {
            var userName = RequestContextManager.Instance.CurrentContext.User.FindFirst("FirstName")?.Value + " " +
                           RequestContextManager.Instance.CurrentContext.User.FindFirst("LastName")?.Value;
            return userName;
        }

        public static string GetRole()
        {
            var role = RequestContextManager.Instance?.CurrentContext?.User
                           .FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value ?? "";
            return role;
        }
    }

    public class RequestContextManager
    {
        public static RequestContextManager Instance { get; set; }

        static RequestContextManager()
        {
            Instance = new RequestContextManager(null);
        }

        private readonly IHttpContextAccessor contextAccessor;

        public RequestContextManager(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public HttpContext CurrentContext => contextAccessor?.HttpContext;
    }
}
