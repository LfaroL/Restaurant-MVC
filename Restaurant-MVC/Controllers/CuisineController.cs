using OdeToFood.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{

    //[Authorize] // User has to be logged in (Redirects to log in)
    //[Log] Filter attribute
    public class CuisineController : Controller
    {

        // GET: /Cuisine/
        public ActionResult Search(string name = "French") // Looks in routing data, query strings, and posed forms to find "name"
        {
            // Server.HtmlEncode renders as text any possibly harmful strings(scripts, SQL injection)
            var message = Server.HtmlEncode(name);
   
            return Content(message);
        }
    }
}


//return RedirectPermanent("http://microsoft.com");
//return RedirectToAction("Index", "Home", new { name = name });
//return RedirectToRoute("Default", new { controller = "Home", action = "About" });
//return Json(new { Message = message, Name = "Scott" }, JsonRequestBehavior.AllowGet );
//return File(Server.MapPath("~/Content/site.css"), "text/css");