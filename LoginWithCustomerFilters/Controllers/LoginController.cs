//using LoginWithCustomFilters.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppStorePractice.Models;
using WebAppStorePractice.Services;


namespace WebAppStorePractice.Controllers
{
    public class LoginController : Controller
    {
        ISecurityService securityService { get; set; }
        IUsersDAO usersDAO { get; set; }

        public LoginController(ISecurityService securitySrv, IUsersDAO usersDao)
        {
            securityService = securitySrv;
            usersDAO = usersDao;
        }

        
        [HttpGet]
        [CustomAuthorization]
        public IActionResult PrivateSectionMustBeLoggedIn()
        {

            return Content("This is private area you must log in to view");
        }
        public IActionResult Index()
        {
            return View();
        }

        [LogActionFilter]
        public IActionResult ProcessLogin(Models.UserModel userModel)
        {
            //MyLogger.GetInstance().Info("Processing Login");
            //MyLogger.GetInstance().Info(userModel.toString());
            //following line is before using dependency injection :)
            //SecurityService securityService = new SecurityService();
            if (securityService.IsValid(userModel))
            {
                HttpContext.Session.SetString("username", userModel.UserName);
              //  MyLogger.GetInstance().Info("Login Success");
                return View("LoginSuccess", userModel);
            }
            else
            {
                HttpContext.Session.Remove("username");
                //MyLogger.GetInstance().Warn("Login Failed");
                return View("LoginFailed", userModel);
            }
            
        }
        public IActionResult Register()
        {
           
                return View("Register");
        
        }
        public IActionResult ProcessRegister(UserModel usermodel)
        {

            if (securityService.IsUnique(usermodel.UserName))
            {
                usersDAO.RegisterNewUser(usermodel);
                HttpContext.Session.SetString("username", usermodel.UserName);
                return View("RegistrationSuccess", usermodel);
            }
            else
            {
                ViewBag.Message = "Username has already been registered! Please Login or choose another username";
                HttpContext.Session.Remove("username");
                return View("Register");
            }
            
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("username");
            return View("Index");
        }
       
    }
}
