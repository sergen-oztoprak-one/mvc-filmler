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
            var results = _context.Movies.ToList();
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
        public async Task<IActionResult> Edit(Movies movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Json(movie);
        }
        [HttpPost]
        public IActionResult Guncelleme (Movies movie)
        {
            if (ModelState.IsValid)
            {
                // Veritabanındaki mevcut kaydı bul ve güncelle
                var existingMovie = _context.Movies.Find(movie.Id);
                if (existingMovie != null)
                {
                    existingMovie.MovieTitle = movie.MovieTitle;
                    existingMovie.Director = movie.Director;
                    existingMovie.MovieDate = movie.MovieDate;
                    existingMovie.MovieDescription = movie.MovieDescription;

                    _context.SaveChanges(); // Değişiklikleri kaydet
                }

                return RedirectToAction("Index"); // Film listesini gösteren sayfaya yönlendir
            }
            return View(movie); // Eğer model geçerli değilse, düzenleme sayfasına geri dön
        }


    }

}