using System;

namespace RemindersAPI.DTOs
{
    public class ShoppingListItemDTO
    {
        public int id { get; set; }

        public string text { get; set; }

        public bool complete { get; set; }

        public DateTime created { get; set; }

        public DateTime lastUpdated { get; set; }
    }
}
