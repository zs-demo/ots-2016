using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.Models
{
    public class MovieWithGenre
    {
        public long ID { get; set; }

        public MovieGenre Genre { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
