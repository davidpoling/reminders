using System;

namespace RemindersDomain.Models
{
    public class Reminder : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeString { get; set; }

        public bool Complete { get; set; }
    }
}
