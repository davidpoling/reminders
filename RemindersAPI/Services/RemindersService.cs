using RemindersAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public interface IRemindersService
    {
        Task<IList<ReminderDTO>> GetReminders();
        Task<ReminderDTO> CreateReminder(ReminderDTO reminder);
        Task<string> DeleteReminder(string reminderId);
    }

    public class RemindersService : IRemindersService
    {
        public Task<IList<ReminderDTO>> GetReminders()
        {
            return null;
        }

        public Task<ReminderDTO> CreateReminder(ReminderDTO reminder)
        {
            return null;
        }

        public Task<string> DeleteReminder(string reminderId)
        {
            return null;
        }
    }
}
