using System;

namespace RemindersDomain.models
{
    public class Reminder
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeString { get; set; }

        public bool Complete { get; set; }
    }
}
