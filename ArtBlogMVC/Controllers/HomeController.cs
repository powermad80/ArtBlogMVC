using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtBlogMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Authenticate(string username, string password)
        {
            string loginusername = "test";
            string loginpassword = "test";

            if (username == loginusername && password == loginpassword)
            {

                GetAuth();
                //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                //HttpContext.Authentication.SignInAsync(
                //    CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(identity));
                
                return Redirect("~/Home/Index.cshtml");
            }
            else
            {
                return new ObjectResult("Login failed");
            }
        }

        public async Task GetAuth()
        {
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal());
        }

        public JsonResult GetPosts(int page = 1)
        {
            List<POST> result = ArtRepo.GetPosts(page);
            foreach (POST i in result)
            {
                if (i.Tags != null)
                    i.Tags = i.Tags.Replace(",", string.Empty);
            }
            return Json(result);
        }
    }
}
