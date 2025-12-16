using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UserModel, IdentityRole<long>, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // === DbSets ===
        public DbSet<Websites> Websites { get; set; }
        public DbSet<PageModel> Pages { get; set; }
        public DbSet<SectionModel> Sections { get; set; }

        public DbSet<Columns> Columns { get; set; }
        public DbSet<Slots> Slots { get; set; }
        public DbSet<ListItems> ListItems { get; set; }


    }
}
