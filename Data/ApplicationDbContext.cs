using Microsoft.EntityFrameworkCore;
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Odd> Odds { get; set; }
        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sport>()
                .HasMany(s => s.Events)
                .WithOne(e => e.Sport)
                .HasForeignKey(e => e.SportID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Event>()
                .HasMany(e => e.Matches)
                .WithOne(m => m.Event)
                .HasForeignKey(m => m.EventID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Match>()
                .HasMany(m => m.Bets)
                .WithOne(b => b.Match)
                .HasForeignKey(b => b.MatchID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Bet>()
                .HasMany(b => b.Odds)
                .WithOne(o => o.Bet)
                .HasForeignKey(o => o.BetID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
