using System;

namespace RemindersAPI.DTOs
{
    public class ReminderDTO
    {
        public string id { get; set; }

        public string text { get; set; }

        public DateTime dateTime { get; set; }

        public string dateTimeString { get; set; }

        public bool complete { get; set; }
    }
}
