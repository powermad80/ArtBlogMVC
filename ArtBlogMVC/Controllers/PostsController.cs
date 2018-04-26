using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ArtBlogMVC.Controllers
{
    public class PostsController : Controller
    {
        // GET: /<controller>/
        public IActionResult img(int id)
        {
            if (CheckAuth())
            {
                return View("~/Views/Posts/AuthPost.cshtml");
            }
            else
            {
                return View("~/Views/Posts/Post.cshtml");
            }
        }

        [HttpPost]
        public JsonResult GetPostData(int id)
        {
            POST result = ArtRepo.GetPost(id);
            if (result.Tags != null)
                result.Tags = result.Tags.Replace(",", string.Empty);
            return Json(result);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult SaveChanges(POST editedPost)
        {

            if (!CheckAuth())
            {
                return new JsonResult("403");
            }

            ArtRepo.UpdatePost(editedPost);

            return new JsonResult("success");
        }

        public bool CheckAuth()
        {
            byte[] val = ConfigData.Key();
            return (HttpContext.Session.TryGetValue(ConfigData.Password(), out val));
        }
    }
}
