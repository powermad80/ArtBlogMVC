using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace ArtBlogMVC.Controllers
{
    public static class DataModules
    {
        public static SqliteConnection DBConnection()
        {
            return new SqliteConnection("Data Source=data.sqlite");
        }
    }

    public class ArtBlogContext : DbContext
    {
        public ArtBlogContext(DbContextOptions<ArtBlogContext> options)
            : base(options)
        { }


    }

    public static class ArtRepo
    {
        public static List<POST> GetPosts(int page = 1)
        {
            List<POST> result;
            using (IDbConnection con = DataModules.DBConnection())
            {
                string query = "SELECT Id, Title, ImgUrl, Description, Tags, Date FROM POSTS ORDER BY Date";

                con.Open();
                result = con.Query<POST>(query).ToList<POST>();
                con.Close();
            }
            return result;
        }
    }

    [Table("POSTS")]
    public class POST
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public DateTime Date { get; set; }
    }
}
