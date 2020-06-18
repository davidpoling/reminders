using RemindersAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public class RemindersService
    {
        public IList<ReminderDTO> GetReminders()
        {
            return new List<ReminderDTO>();
        }

        public ReminderDTO AddReminder(ReminderDTO reminder)
        {
            return reminder;
        }

        public string DeleteReminder(string reminderId)
        {
            return reminderId;
        }
    }
}
