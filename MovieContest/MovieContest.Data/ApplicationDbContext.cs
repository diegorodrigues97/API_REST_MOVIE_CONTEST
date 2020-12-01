using Microsoft.EntityFrameworkCore;
using MovieContest.Domain.Entities;

namespace MovieContest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Contest> MovieContest { get; set; }
        public DbSet<Actor> Actor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.LastName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Gender).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Token).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(p => p.RefreshToken).HasMaxLength(255);
            modelBuilder.Entity<User>().Property(p => p.FlagDeleted).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<User>().Property(p => p.PersistDate).IsRequired();

            //Actor
            modelBuilder.Entity<Actor>().HasKey(p => p.Id);
            modelBuilder.Entity<Actor>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Actor>().Property(p => p.FullName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Actor>().Property(p => p.NickName).HasMaxLength(100);
            modelBuilder.Entity<Actor>().Property(p => p.Nationality).HasMaxLength(80);
            modelBuilder.Entity<Actor>().Property(p => p.Awards).HasMaxLength(255);
            modelBuilder.Entity<Actor>().Property(p => p.FlagDeleted).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<Actor>().Property(p => p.PersistDate).IsRequired();

            //Movie
            modelBuilder.Entity<Movie>().HasKey(p => p.Id);
            modelBuilder.Entity<Movie>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Movie>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Movie>().Property(p => p.Director).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Movie>().Property(p => p.FlagDeleted).HasDefaultValue(false).IsRequired();
            modelBuilder.Entity<Movie>().Property(p => p.PersistDate).IsRequired();

            //Role
            modelBuilder.Entity<Role>().HasKey(p => p.Id);
            modelBuilder.Entity<Role>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Role>().Property(p => p.CharacterName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Role>().Property(p => p.FlagMainActor).HasDefaultValue(true).IsRequired();
            modelBuilder.Entity<Role>().Property(p => p.FlagDeleted).HasDefaultValue(true).IsRequired();
            modelBuilder.Entity<Role>().Property(p => p.PersistDate).IsRequired();

            //Movie Contest
            modelBuilder.Entity<Contest>().HasKey(p => p.Id);
            modelBuilder.Entity<Contest>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contest>().Property(p => p.PersistDate).IsRequired();
            modelBuilder.Entity<Contest>().Property(p => p.FlagDeleted).HasDefaultValue(true).IsRequired();
        }
    }
}
