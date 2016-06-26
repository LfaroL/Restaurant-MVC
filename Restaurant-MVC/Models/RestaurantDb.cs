using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    // DbContext
    // Looks at LocalDB and automatically create database 
    // with same name as DbContext class on build
    public class RestaurantDb : DbContext
    {
        // Points to a specific database connection
        // Change at Web.config to point to new database
        public RestaurantDb() : base("name=DefaultConnection")
        {

        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantReview> Reviews { get; set; }

    }


    public interface IRestaurantDb : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
    }

    // To add a database connection
    // View -> Server Explorer
    // Data Connections -> Add connection
    // Microsoft SQL Server
    // Server name: (Localdb)\v11.0

    // In case:
    // delete database mdf file in App_Data to recreate at build
}