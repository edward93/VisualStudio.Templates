﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using $safeprojectname$.Infrastructure;
using $ext_safeprojectname$.Infrastructure.Enums;
using $ext_safeprojectname$.Infrastructure.Helpers;

namespace $safeprojectname$.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger Logger;
        protected readonly AppSettings Settings;  
        protected readonly IHostingEnvironment Environment;      

        public BaseController(ILoggerFactory loggerFactory, IOptions<AppSettings> settings, IHostingEnvironment environment)
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

        protected void CreateSuccessResult(ServiceResult serviceResult, object data, string message)
        {
            serviceResult.Success = true;
            serviceResult.Data = data;
            serviceResult.Messages.AddMessage(MessageType.Info, message);
        }

        protected string GetUserId()
        {
            var userId = User.Claims.Where(c => c.Type == "userId").Select(c => c.Value).FirstOrDefault();
            return userId;
        }

        protected async Task<IActionResult> MakeActionCall<TResult>(Func<Task<TResult>> action)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var result = await action();
                CreateSuccessResult(serviceResult, result, "OK");
            }
            catch (Exception e)
            {
                CreateErrorResult(serviceResult, e);
            }

            return Json(serviceResult);
        }
    }
}