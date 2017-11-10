using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using $ext_safeprojectname$.Infrastructure.Enums;
using $ext_safeprojectname$.Infrastructure.Helpers;

namespace $safeprojectname$.Controllers
{
    public class BaseController : Controller
    {
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

        protected string GetPerson()
        {
            var personId = User.Claims.Where(c => c.Type == "personId").Select(c => c.Value).FirstOrDefault();
            return personId;
        }
    }
}