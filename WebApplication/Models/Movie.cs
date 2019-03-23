using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public string Genre { get; set; }

        public DateTime ReleasedDate { get; set; }
    }
}
