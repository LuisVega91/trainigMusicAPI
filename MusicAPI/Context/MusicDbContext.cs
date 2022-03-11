using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Context
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Album> Albums{ get; set; }
        public DbSet<Band> Bands{ get; set; }
    }
}
