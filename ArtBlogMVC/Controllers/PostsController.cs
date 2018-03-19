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
            return View("~/Views/Posts/Post.cshtml");
        }

        [HttpPost]
        public JsonResult GetPostData(int id)
        {
            POST result = ArtRepo.GetPost(id);
            if (result.Tags != null)
                result.Tags = result.Tags.Replace(",", string.Empty);
            return Json(result);
        }
    }
}
