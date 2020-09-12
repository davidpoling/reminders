using Microsoft.EntityFrameworkCore;
using RemindersDomain.infrastructure;
using RemindersDomain.models;

namespace RemindersDomain
{
    public class ReminderContext : DbContext
    {
        public ReminderContext(DbContextOptions options) : base(options) { }

        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReminderEntityTypeConfiguration());
        }
    }
}
