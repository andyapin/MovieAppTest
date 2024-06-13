using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppTest.Models
{
    public class MovieGenre
    {
        public List<MovieGenreList> genres { get; set; }
    }

    public class MovieGenreList
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
