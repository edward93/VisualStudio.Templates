using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using $safeprojectname$.Infrastructure;
using $ext_safeprojectname$.Infrastructure.Enums;
using $ext_safeprojectname$.Infrastructure.Helpers;

namespace $safeprojectname$.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger Logger;
        protected readonly AppSettings Settings;

        public BaseController(ILoggerFactory loggerFactory, IOptions<AppSettings> settings)
        {
            Logger = loggerFactory.CreateLogger("Controller");
            Settings = settings.Value;
        }

        protected string GetModelStateErrors()
        {
            return string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }

        protected void CreateErrorResult(ServiceResult serviceResult, Exception ex)
        {
            serviceResult.Success = false;
            serviceResult.Messages.AddMessage(MessageType.Error, ex.Message);
        }

        protected long GetUserId()
        {
            long id = -1;
            var userId = User.Claims.Where(c => c.Type == "userId").Select(c => c.Value).FirstOrDefault();
            long.TryParse(userId, out id);
            return id;
        }
    }
}