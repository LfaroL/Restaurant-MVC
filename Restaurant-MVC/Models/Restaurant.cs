using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    // Restaurant Entity
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ICollection<RestaurantReview> Reviews { get; set; }
        // Create wrapper for Restaurant class at runtime and intercept recalls
        // to the reviews property. Make sure it loads and requires a second query
        // to the database
    }
}