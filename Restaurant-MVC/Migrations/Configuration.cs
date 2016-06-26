namespace OdeToFood.Migrations
{
    using OdeToFood.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.RestaurantDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        // To automatically create this migration configuration file:
        // On Package Manager Console: Enable-Migrations -ContextTypeName OdeToFoodDb

        // To update the database
        // On Package Manager Console: Update-Database -Verbose

        // Populate initial data with seed method
        protected override void Seed(RestaurantDb context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
               new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
               new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
               new Restaurant
               {
                   Name = "Smaka",
                   City = "Gothenburg",
                   Country = "Sweden",
                   Reviews =
                       new List<RestaurantReview> {
                       new RestaurantReview { Rating = 9, Body="Great food!", ReviewerName = "Scott" }
                   }
               });

            //for (int i = 0; i < 1000; i++)
            //{
            //    context.Restaurants.AddOrUpdate(r => r.Name,
            //        new Restaurant { Name = i.ToString(), City = "Nowhere", Country = "USA" });
            //}

            SeedMembership();
        }

        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("betterUser",false) == null)
            {
                membership.CreateUserAndAccount("betterUser", "betterPassword");
            }
            if (!roles.GetRolesForUser("betterUser").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "betterUser" }, new[] { "admin" });
            }
        }
    }
}
