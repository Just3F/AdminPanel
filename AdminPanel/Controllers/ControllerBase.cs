using System;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ControllerBase : Controller
    {
        protected JsonResult SuccessJsonResult()
        {
            return Json(new { Result = "ok" });
        }

        protected JsonResult InvokeException(Exception ex)
        {
            return Json(new
            {
                HasErrors = true,
                Errors = ex.Message
            });
        }

        protected JsonResult InvokeError(string message)
        {
            return Json(new
            {
                HasError = true,
                Error = message
            });
        }
    }
}
