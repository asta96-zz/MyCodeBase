using vidly.Models;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace vidly.Models
{
    public class customer
    {
        public string Name { get; set; }
        public int id { get; set; }
    }
    public class ViewModel
    {
        public Movies Movies { get; set; }
        public List<customer> Customers { get; set; }
    }
    public class Customers
    {
        public List<customer> customers { get; set; }

    }
}