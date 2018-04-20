using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtBlogMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            byte[] val = BitConverter.GetBytes(1);
            if (CheckAuth())
            {
                return View("~/Views/Home/Admindex.cshtml");
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
            
        }

        //[Authorize]
        public IActionResult Admindex()
        {
            return View("~/Views/Home/Admindex.cshtml");
        }

        public IActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("auth");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult NewPost()
        {
            if (CheckAuth())
            {
                return View("~/Views/Home/NewPost.cshtml");
            }
            else
            {
                return View("~/Views/Home/Login.cshtml");
            }
        }

        public bool CheckAuth()
        {
            byte[] val = BitConverter.GetBytes(1);
            return (HttpContext.Session.TryGetValue("auth", out val));
        }

        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {
            string loginusername = "test";
            string loginpassword = "test";

            if (username == loginusername && password == loginpassword)
            {
                HttpContext.Session.Set("auth", BitConverter.GetBytes(1));
                return RedirectToAction("Index");
            }
            else
            {
                return View("~/Views/Home/LoginFailed.cshtml");
            }
        }

        public async Task GetAuth()
        {
            await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal());
        }

        [HttpPost]
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

        [HttpPost]
        public IActionResult AddPost()
        {

            IFormFile pic = HttpContext.Request.Form.Files[0];
            string extension = Path.GetExtension(pic.FileName);
            POST newPost = new POST();
            newPost.Description = HttpContext.Request.Form["Description"];
            newPost.Title = HttpContext.Request.Form["Title"];
            newPost.Tags = HttpContext.Request.Form["Tags"];
            newPost.Date = DateTime.Now;
            string filename = "pics/" + DateTime.Now.Ticks.ToString() + extension;
            using (FileStream fs = System.IO.File.Create("wwwroot/" + filename))
            {
                pic.CopyTo(fs);
                fs.Flush();
            }
            newPost.ImgUrl = filename;
            ArtRepo.AddPost(newPost);
            return RedirectToAction("Index");

        }
    }
}
