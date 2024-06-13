using MovieAppTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppTest.Controllers
{
    public class GlobalController
    {
        public static List<MovieGenreList> Genres { get; set; } = new List<MovieGenreList>();
    }
}
