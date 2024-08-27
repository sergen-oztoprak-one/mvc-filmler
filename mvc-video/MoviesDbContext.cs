    using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using mvc_video.Models;
namespace mvc_video
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options)
        {
        }

        public DbSet<Movies> Movies { get; set; }
    }
}
