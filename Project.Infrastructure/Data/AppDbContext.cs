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
        public DbSet<LayoutSection> LayoutSections { get; set; }
        public DbSet<LayoutSlot> LayoutSlots { get; set; }
        public DbSet<ComponentModel> Components { get; set; }
        public DbSet<ComponentProp> ComponentProps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ================
            // Websites → Pages
            // ================
            builder.Entity<Websites>()
                .HasMany(w => w.Pages)
                .WithOne(p => p.websites)
                .HasForeignKey(p => p.WebsitesId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==================
            // Page → Sections
            // ==================
            builder.Entity<PageModel>()
                .HasMany(p => p.Sections)
                .WithOne(s => s.Page)
                .HasForeignKey(s => s.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            // =====================================
            // SectionModel → LayoutSection (optional)
            // =====================================
            builder.Entity<SectionModel>()
                .HasOne(s => s.Layout)
                .WithOne(l => l.Section)
                .HasForeignKey<LayoutSection>(l => l.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============================
            // LayoutSection → LayoutSlots
            // ============================
            builder.Entity<LayoutSection>()
                .HasMany(l => l.Slots)
                .WithOne(s => s.LayoutSection)
                .HasForeignKey(s => s.LayoutSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============================
            // LayoutSlot → Components
            // ============================
            builder.Entity<LayoutSlot>()
                .HasMany(s => s.Components)
                .WithOne(c => c.Slot)
                .HasForeignKey(c => c.SlotId)
                .OnDelete(DeleteBehavior.Cascade);

            // ============================
            // ComponentModel → Props
            // ============================
            builder.Entity<ComponentModel>()
                .HasMany(c => c.Props)
                .WithOne(p => p.Component)
                .HasForeignKey(p => p.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional unique indexes
            builder.Entity<ComponentProp>()
                .HasIndex(p => new { p.ComponentId, p.Key })
                .IsUnique();
        }
    }
}
