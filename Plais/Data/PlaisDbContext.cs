using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plais.Models;
using PLAIS.API.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PLAIS.API.Data
{
    public class PlaisDbContext(DbContextOptions<PlaisDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<ExecutiveMember> ExecutiveMembers { get; set; }
        public DbSet<Cadence> Cadences { get; set; }
        public DbSet<CadenceMembership> CadenceMemberships { get; set; }
        public DbSet<ByLaws> ByLaws { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Bulletin> Bulletins { get; set; }
        public DbSet<BulletinPhoto> BulletinPhoto { get; set; }
        public DbSet<FoundingMembers> FoundingMembers { get; set; }
        public DbSet<CurrentMembers> CurrentMembers { get; set; }
        public DbSet<EventGroup> EventGroups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ResourceCategory> ResourceCategories { get; set; }
        public DbSet<ResourceGroup> ResourceGroups { get; set; }
        public DbSet<ResourceItem> ResourceItems { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<AchievementImage> AchievementImages { get; set; }
        public DbSet<MainPageText> MainPageTexts { get; set; }
        public DbSet<MainPageCarousel> MainPageCarouselImages { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CadenceMembership>()
                .HasKey(cm => new { cm.ExecutiveMemberId, cm.CadenceId });

            modelBuilder.Entity<CadenceMembership>()
                .HasOne(cm => cm.ExecutiveMember)
                .WithMany(em => em.Memberships)
                .HasForeignKey(cm => cm.ExecutiveMemberId);

            modelBuilder.Entity<CadenceMembership>()
                .HasOne(cm => cm.Cadence)
                .WithMany(c => c.Members)
                .HasForeignKey(cm => cm.CadenceId);

            modelBuilder.Entity<Bulletin>()
                .HasMany(b => b.Photos)
                .WithOne()
                .HasForeignKey(p => p.BulletinId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventGroup>()
                .HasMany(eg => eg.Events)
                .WithOne()
                .HasForeignKey(e => e.EventGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResourceGroup>()
                .HasOne<ResourceCategory>()
                .WithMany(rc => rc.Groups)
                .HasForeignKey(rg => rg.ResourceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResourceItem>()
                .HasOne<ResourceGroup>()
                .WithMany(rg => rg.Items)
                .HasForeignKey(ri => ri.ResourceGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Achievement>()
                .HasMany(a => a.Images)
                .WithOne()
                .HasForeignKey(i => i.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);



        }


    }
}
