using System;

namespace RemindersAPI.DTOs
{
    public class ReminderDTO
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeString { get; set; }
    }
}
