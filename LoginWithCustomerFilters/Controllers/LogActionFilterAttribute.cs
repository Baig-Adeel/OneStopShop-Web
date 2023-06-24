using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebAppStorePractice.Models;
using WebAppStorePractice.Utility;

namespace WebAppStorePractice.Controllers
{
    internal class LogActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            UserModel user = (UserModel)((Controller)context.Controller).ViewData.Model;
           
            string userName = context.HttpContext.Session.GetString("username");
            if (userName == null)
            {
                MyLogger.GetInstance().Warn("Logging Attempt Failed: " + user.toString());
            }
            else
            {
                MyLogger.GetInstance().Info("Logging Attempted Successfull: " + user.toString());

            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            MyLogger.GetInstance().Info("Entering Logging authentication");
        }
    }
}