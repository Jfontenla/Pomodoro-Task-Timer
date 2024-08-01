using Microsoft.EntityFrameworkCore;
using Pomodoro.Domain.Entities;
using Labor = Pomodoro.Domain.Entities.Task;

namespace Pomodoro.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Labor> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskDuration> TasksDuration { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Labor>().HasKey(t=>t.Id);
            modelBuilder.Entity<TaskDuration>().HasKey(t=>t.Id);
            modelBuilder.Entity<User>().HasKey(t=>t.Id);

            modelBuilder.Entity<TaskDuration>()
                .HasOne(d => d.Task)
                .WithMany(t => t.TasksDuration).HasForeignKey(d => d.TaskId);

            modelBuilder.Entity<TaskDuration>()
                .HasOne(d => d.User)
                .WithMany(t => t.TasksDuration)
                .HasForeignKey(d => d.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
