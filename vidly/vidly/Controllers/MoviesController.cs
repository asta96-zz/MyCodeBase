using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly.Models;
namespace vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
       public ActionResult Released()
        {
            var movies = new List<Movies>()
            {
               new Movies() { Name = "Sheek!",id = 1},
               new Movies() { Name = "Wall-E!",id = 2}
    };
            //var movies = FetchCustomer();
            var movielist = new MovieList();
            movielist.Movies = movies;
            return View(movielist);
        }
        public List<customer> FetchCustomer()
        {
             var customers = new List<customer>()
            {
                new customer(){Name="Dinesh",id=1},
                new customer(){Name="Ganesh",id=2}
            };
            return customers;
}
        public ActionResult Customers()
        {

            var customerList = FetchCustomer();
            
            return View(customerList);
        }

        public ActionResult Details(int id)
        {
            var customer = FetchCustomer().Where(x => x.id == id).FirstOrDefault();
            if(customer!= null)
            return View(customer);
            else
            {
                return Content("No customer found for this ID");
            }
        }
    }
}