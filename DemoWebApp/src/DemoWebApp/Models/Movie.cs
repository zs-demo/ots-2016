using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.Models
{
    public class Movie
    {
        [Key]
        public long ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImdbLink { get; set; }
        public string PosterLink { get; set; }

        public virtual ICollection<MovieWithGenre> Genres { get; set; }
    }
}
