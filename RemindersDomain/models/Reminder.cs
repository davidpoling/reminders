using System;

namespace RemindersDomain.models
{
    public class Reminder
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeString { get; set; }
    }
}
