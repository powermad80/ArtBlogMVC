﻿using System;
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
        public static List<POST> GetPosts()
        {
            List<POST> result;
            using (IDbConnection con = DataModules.DBConnection())
            {
                string query = "SELECT Id, Title, ImgUrl, Description, Tags, Date FROM POSTS WHERE Deleted = 0 ORDER BY Date DESC";

                con.Open();
                result = con.Query<POST>(query).ToList<POST>();
                con.Close();
            }
            return result;
        }

        public static List<POST> SearchPosts(string term)
        {
            List<POST> result;
            using (IDbConnection con = DataModules.DBConnection())
            {
                string query = "SELECT Id, Title, ImgUrl, Description, Tags, Date FROM POSTS WHERE Deleted = 0 AND Tags LIKE '%" + term + "%' ORDER BY Date DESC";

                con.Open();
                result = con.Query<POST>(query).ToList<POST>();
                con.Close();
            }
            return result;
        }

        public static POST GetPost(int id)
        {
            List<POST> result;
            using (IDbConnection con = DataModules.DBConnection())
            {
                string query = "SELECT Title, ImgUrl, Description, Tags, Date FROM POSTS WHERE Id = " + id;
                con.Open();
                result = con.Query<POST>(query).ToList<POST>();
                con.Close();
            }
            return result.First<POST>();
        }

        public static void AddPost(POST newPost)
        {
            using (IDbConnection con = DataModules.DBConnection())
            {
                con.Open();
                con.Insert<POST>(newPost);
                con.Close();
            }
            return;
        }

        public static void UpdatePost(POST editedPost)
        {

            if (editedPost.Description == null)
            {
                editedPost.Description = "";
            }

            if (editedPost.Tags == null)
            {
                editedPost.Tags = "";
            }

            if (editedPost.Title == null)
            {
                editedPost.Title = "";
            }

            using (SqliteConnection con = DataModules.DBConnection())
            {
                string query = "UPDATE POSTS SET Description = @description, Tags = @tags, Title = @title, Deleted = @deleted WHERE Id = @id";
                SqliteCommand c = new SqliteCommand(query, con);
                c.Parameters.AddWithValue("@description", editedPost.Description);
                c.Parameters.AddWithValue("@tags", editedPost.Tags);
                c.Parameters.AddWithValue("@title", editedPost.Title);
                c.Parameters.AddWithValue("@deleted", editedPost.Deleted);
                c.Parameters.AddWithValue("@id", editedPost.Id);
                con.Open();
                c.ExecuteNonQuery();
                con.Close();
            }
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

        public int Deleted { get; set; }

        public DateTime Date { get; set; }
    }
}
