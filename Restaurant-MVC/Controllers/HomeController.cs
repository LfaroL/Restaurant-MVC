using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Configuration;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        // Initialize the database model
        RestaurantDb _db = new RestaurantDb();
        IRestaurantDb _db2;

        public HomeController()
        {
            _db = new RestaurantDb();
        }

        public HomeController(IRestaurantDb db)
        {
            _db2 = db;
        }
        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.Restaurants
                    .Where(r => r.Name.StartsWith(term))
                    .Take(10)
                    .Select(r => new
                    {                   // Projection with select: turn every restaurant into an object
                        label = r.Name  // with a label property equal to the restaurant name
                                        // jQuery UI autocomplete requires the object returned have a label or value property
                    });
            // Convert model to JSON format, allow to happen on a Get request
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        [OutputCache(Duration =60)]
        public ActionResult SayHello()
        {
            return Content("Hello");
        }

        //[AllowAnonymous]
        [OutputCache(Duration = 60)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {

            var model =
                _db.Restaurants
                    .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                    .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm)) // if searchTerm == null, it will return true and return all restaurants
                    .Select(r => new RestaurantListViewModel // Returns a new object 
                    {
                        Id = r.Id,
                        Name = r.Name,
                        City = r.City,
                        Country = r.Country,
                        CountOfReviews = r.Reviews.Count() // New anonymous type
                    }).ToPagedList(page, 10);     // IPagedList pagination of lists

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            ViewBag.MailServer = ConfigurationManager.AppSettings["MailServer"];
            return View(model);
        }


        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Scott";
            model.Location = "Maryland, USA";
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


//Index
//var controller = RouteData.Values["controller"];
//var action = RouteData.Values["action"];
//var id = RouteData.Values["id"];

//var message = String.Format("{0}::{1} {2}", controller, action, id);

//ViewBag.Message = message;

//var model =
//    from r in _db.Restaurants
//    orderby r.Reviews.Average(review => review.Rating) descending
//    select new RestaurantListViewModel // Returns a new object 
//    {
//        Id = r.Id,
//        Name = r.Name,
//        City = r.City,
//        Country = r.Country,
//        CountOfReviews = r.Reviews.Count() // New anonymous type
//    };
