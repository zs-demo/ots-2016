using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoWebApp.Data;
using Microsoft.AspNetCore.Identity;
using DemoWebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DemoWebApp.Areas.FilmFanatic.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoWebApp.Areas.FilmFanatic.Controllers
{
    [Area("FilmFanatic")]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private ILogger logger;

        public MoviesController(ILoggerFactory logFactory, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.logger = logFactory.CreateLogger<MoviesController>();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("api/movies/find/")]
        public IActionResult FindMovies(string query = null)
        {
            try
            {
                var data = dbContext.TblMovies.Include(t => t.Genres) as IQueryable<Movie>;

                if (string.IsNullOrEmpty(query) == false)
                {
                    var q = query.ToLower();
                    data = data.Where(i => i.Title.ToLower().Contains(q) || i.Description.ToLower().Contains(q));
                }

                var result = from i in data
                             select new
                             {
                                 ID = i.ID,
                                 Title = i.Title,
                                 Description = i.Description,
                                 ImdbLink = i.ImdbLink,
                                 PosterLink = i.PosterLink,
                                 Genres = (from j in i.Genres select j.Genre.ToString())
                             };

                return Ok(result);
            }
            catch (Exception exc)
            {
                logger.LogError("Error searching movies...", exc);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("api/movies/{genre}/filter/")]
        public IActionResult FilterMoviesByGenre(string genre, string query = null)
        {
            try
            {
                var data = dbContext.TblMovies
                    .Include(t => t.Genres)
                    .Where(t => t.Genres.Any(i => i.Genre.ToString() == genre));

                if (string.IsNullOrEmpty(query) == false)
                {
                    var q = query.ToLower();
                    data = data.Where(i => i.Title.ToLower().Contains(q) || i.Description.ToLower().Contains(q));
                }

                var result = from i in data
                             select new
                             {
                                 ID = i.ID,
                                 Title = i.Title,
                                 Description = i.Description,
                                 ImdbLink = i.ImdbLink,
                                 PosterLink = i.PosterLink,
                                 Genres = (from j in i.Genres select j.Genre.ToString())
                             };

                return Ok(result);
            }
            catch (Exception exc)
            {
                logger.LogError("Error filtering movies...", exc);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("api/movies/create")]
        public IActionResult MovieCreate([FromBody] CreateViewModel data)
        {
            try
            {
                var movie = new Movie
                {
                    Title = data.Title,
                    Description = data.Description,
                    ImdbLink = data.ImdbLink,
                    PosterLink = data.PosterLink
                };

                var movieGenres = new List<MovieWithGenre>();
                foreach (var i in data.Genres)
                {
                    movieGenres.Add(new MovieWithGenre
                    {
                        Movie = movie,
                        Genre = (MovieGenre)Enum.Parse(typeof(MovieGenre), i)
                    });
                }

                dbContext.TblMovies.Add(movie);
                dbContext.TblMoviesWithGenres.AddRange(movieGenres);
                dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception exc)
            {
                logger.LogError("Errors creating new movie...", exc);
            }

            return BadRequest();
        }

        public IActionResult Edit(long id)
        {
            var obj = dbContext.TblMovies.Include(t => t.Genres).SingleOrDefault(i => i.ID == id);

            if (obj != null)
            {
                var model = new EditViewModel
                {
                    ID = obj.ID,
                    Title = obj.Title,
                    Description = obj.Description,
                    ImdbLink = obj.ImdbLink,
                    PosterLink = obj.PosterLink,
                    Genres = (from i in obj.Genres select i.Genre.ToString()).ToArray()
                };

                return PartialView(model);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("api/movies/edit")]
        public IActionResult Edit(long id, [FromBody]EditViewModel data)
        {
            try
            {
                var obj = dbContext.TblMovies.SingleOrDefault(i => i.ID == id);
                obj.Title = data.Title;
                obj.Description = data.Description;
                obj.ImdbLink = data.ImdbLink;
                obj.PosterLink = data.PosterLink;

                var oldGenres = dbContext.TblMoviesWithGenres.Include(t => t.Movie).Where(i => i.Movie.ID == id);
                dbContext.TblMoviesWithGenres.RemoveRange(oldGenres);

                var newGenres = new List<MovieWithGenre>();
                foreach (var i in data.Genres)
                {
                    newGenres.Add(new MovieWithGenre
                    {
                        Movie = obj,
                        Genre = (MovieGenre)Enum.Parse(typeof(MovieGenre), i)
                    });
                }
                dbContext.TblMoviesWithGenres.AddRange(newGenres);

                dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception exc)
            {
                logger.LogError("Errors editing new movie...", exc);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("api/movies/delete")]
        public IActionResult Delete(long id)
        {
            try
            {
                var obj = dbContext.TblMovies.Include(t => t.Genres).SingleOrDefault(i => i.ID == id);
                dbContext.TblMovies.Remove(obj);

                dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception exc)
            {
                logger.LogError("Errors deleting a movie...", exc);
            }

            return BadRequest();
        }
    }
}
