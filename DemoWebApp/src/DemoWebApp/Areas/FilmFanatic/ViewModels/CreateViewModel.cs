using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.Areas.FilmFanatic.ViewModels
{
    public class CreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImdbLink { get; set; }
        public string PosterLink { get; set; }

        public string[] Genres { get; set; }
    }
}
