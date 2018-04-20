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

        [HttpPost]
        public IActionResult SaveChanges()
        {

            if (!CheckAuth())
            {
                return RedirectToAction("Home/Login");
            }

            POST editedPost = new POST();
            editedPost.Id = Int32.Parse(HttpContext.Request.Form["postId"]);
            editedPost.Title = HttpContext.Request.Form["title"];
            editedPost.Description = HttpContext.Request.Form["description"];
            editedPost.Tags = HttpContext.Request.Form["tags"];
            if (HttpContext.Request.Form.ContainsKey("delete"))
            {
                editedPost.Deleted = Int32.Parse(HttpContext.Request.Form["delete"]);
            }
            //else
            //{
            //    editedPost.Deleted = 0;
            //}

            ArtRepo.UpdatePost(editedPost);

            if (editedPost.Deleted == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("img");
            }
        }

        public bool CheckAuth()
        {
            byte[] val = BitConverter.GetBytes(1);
            return (HttpContext.Session.TryGetValue("auth", out val));
        }
    }
}
