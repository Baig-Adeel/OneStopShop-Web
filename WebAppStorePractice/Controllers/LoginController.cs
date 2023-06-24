using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppStorePractice.Models;
using WebAppStorePractice.Services;
using WebAppStorePractice.Utility;

namespace WebAppStorePractice.Controllers
{
    public class LoginController : Controller
    {
       
        public IActionResult Index()

        {
            return View();
        }
        public IActionResult ProcessLogin(Models.UserModel userModel)
        {
            MyLogger.GetInstance().Info("Processing Login");
            MyLogger.GetInstance().Info(userModel.toString());
            SecurityService securityService = new SecurityService();
            if (securityService.IsValid(userModel))
            {
                MyLogger.GetInstance().Info("Login Success");
                return View("LoginSuccess", userModel);
            }
            else
            {
                MyLogger.GetInstance().Warn("Login Failed");
                return View("LoginFailed", userModel);
            }
            
        }
       
    }
}
