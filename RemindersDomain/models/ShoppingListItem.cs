namespace RemindersDomain.Models
{
    public class ShoppingListItem : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
