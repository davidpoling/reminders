namespace RemindersDomain.Models
{
    public class ShoppingListItem : Entity
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
