using System;

namespace RemindersDomain.Models
{
    public class ShoppingListItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
