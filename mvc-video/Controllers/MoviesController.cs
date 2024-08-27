using Microsoft.AspNetCore.Mvc;
using mvc_video.Models;

namespace mvc_video.Controllers
{
    public class MoviesController : Controller
    {
        // Dependency Injection
        private readonly MoviesDbContext _context;
        public MoviesController(MoviesDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var results =  _context.Movies.ToList();
            if (results is null)
            {
                return BadRequest("Herhangi bir film bulunamadı");
            }
            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie([Bind("MovieTitle", "MovieDescription", "Director", "MovieDate")] Movies movie)
        {
            if (ModelState.IsValid)
            {
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Öğrenciler listesine yönlendirir
            }
            return View(movie);
        }

    }
}
