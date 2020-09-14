using System;

namespace RemindersAPI.Commands
{
    public class CreateReminderCommand
    {
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public string DateTimeString { get; set; }
    }
}
