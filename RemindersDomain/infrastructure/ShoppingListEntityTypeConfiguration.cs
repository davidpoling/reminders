using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemindersDomain.Models;

namespace RemindersDomain.Infrastructure
{
    class ShoppingListEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingListItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingListItem> builder)
        {
            builder.ToTable("ShoppingList", null);
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Text);
            builder.Property(s => s.Checked);
        }
    }
}
