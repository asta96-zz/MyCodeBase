using System.Collections.Generic;

namespace vidly.Models
{
    public class Movies
    {
        public string Name { get; set; }
        public int id { get; set; }
    }

    public class MovieList
    {
        public List<Movies> Movies { get; set; }
    }
}