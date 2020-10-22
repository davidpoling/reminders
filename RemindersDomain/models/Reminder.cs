using System;

namespace RemindersDomain.Models
{
    public class Reminder : Entity
    {
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeString { get; set; }

        public bool Complete { get; set; }
    }
}
