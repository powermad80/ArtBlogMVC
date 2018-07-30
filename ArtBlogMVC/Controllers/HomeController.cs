using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
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

        public IActionResult Commissions()
        {
            return View();
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
            byte[] val = ConfigData.Key();
            return (HttpContext.Session.TryGetValue(ConfigData.Password(), out val));
        }

        [HttpPost]
        public IActionResult Authenticate(string username, string password)
        {

            if (username == ConfigData.Username() && password == ConfigData.Password())
            {
                HttpContext.Session.Set(ConfigData.Password(), ConfigData.Key());
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
        public JsonResult GetPosts(int page = 1, string search = "")
        {
            List<POST> result;
            if (search == null)
            {
                result = ArtRepo.GetPosts();
            }
            else
            {
                result = ArtRepo.SearchPosts(search);
            }
            
            int start = 10 * (page - 1);
            int end = (10 * page) - 1;
            bool leftArrow = true;
            bool rightArrow = false;

            if (page > 1)
            {
                leftArrow = false;
            }

            if (result.Count - 1 == end)
            {
                rightArrow = true;
            }

            if (result.Count - 1 < start)
            {
                start = result.Count - 1;
            }

            if (result.Count - 1 < end)
            {
                end = result.Count - 1;
                rightArrow = true;
            }

            var ArrowState = new
            {
                rightArrow = rightArrow,
                leftArrow = leftArrow
            };

            List<POST> Posts = result.GetRange(start, (end + 1) - start);

            foreach (POST i in Posts)
            {
                if (i.Tags != null)
                    i.Tags = i.Tags.Replace(",", string.Empty);
            }

            var Send = new
            {
                Posts = Posts,
                ArrowState = ArrowState
            };

            return Json(Send);
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
            newPost.Deleted = 0;
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
