using M7CarManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace M7CarManager.Data
{
    public class ApiDbContext : IdentityDbContext<AppUser>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> opt) : base(opt)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
              new { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
              new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            AppUser kovi = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "kovi91@gmail.com",
                EmailConfirmed = true,
                UserName = "kovi91@gmail.com",
                FirstName = "Kovács",
                LastName = "András",
                NormalizedUserName = "KOVI91@GMAIL.COM"
            };
            kovi.PasswordHash = ph.HashPassword(kovi, "almafa123");
            builder.Entity<AppUser>().HasData(kovi);

            builder.Entity<Car>().HasData(new Car()
            {
                Model = "Opel Astra",
                PlateNumber = "ABC-123",
                Price = 2000,
                OwnerId = kovi.Id
            });

            builder.Entity<Car>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }


    }
}
