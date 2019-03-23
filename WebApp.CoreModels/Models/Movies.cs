using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.CoreModels.Models
{
    public class Movies
    {
        public string Name { get; set; }
        public string Genre { get; set; }

        public DateTime ReleasedDate { get; set; }
    }

    public class MovieSearchHistory {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime SearcherOn { get; set; }
    }
}
