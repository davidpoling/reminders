using RemindersAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public interface IReminderService
    {
        Task<IList<ReminderDTO>> GetReminders();
        Task<ReminderDTO> CreateReminder(ReminderDTO reminder);
        Task<string> DeleteReminder(string reminderId);
    }

    public class ReminderService : IReminderService
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
