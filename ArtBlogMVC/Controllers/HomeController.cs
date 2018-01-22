using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtBlogMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
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
