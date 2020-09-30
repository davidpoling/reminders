using Microsoft.EntityFrameworkCore;
using RemindersDomain.infrastructure;
using RemindersDomain.Infrastructure;
using RemindersDomain.Models;

namespace RemindersDomain
{
    public class ReminderContext : DbContext
    {
        public ReminderContext(DbContextOptions options) : base(options) { }

        public DbSet<Reminder> Reminders { get; set; }

        public DbSet<ShoppingListItem> ShoppingList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReminderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingListEntityTypeConfiguration());
        }
    }
}
