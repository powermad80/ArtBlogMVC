using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace ArtBlogMVC.Controllers
{
    public class DataModules
    {
        
    }

    public static class ArtRepo
    {

    }

    [Table("POST")]
    public class POST
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }
    }
}
