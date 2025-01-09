using Autokereskedes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autokereskedes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Profile> Profiles { get; set; }

       /* protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Profile>()
                   .HasOne(p => p.User)
                   .WithMany() // Egy felhasználónak egy profilja lehet
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Törlési viselkedés
        }*/
    }
}
