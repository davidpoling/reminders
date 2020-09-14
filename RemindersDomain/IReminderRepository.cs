using Microsoft.EntityFrameworkCore;
using RemindersDomain.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindersDomain
{
    public interface IReminderRepository
    {
        Task<IList<Reminder>> GetReminders();
        Task<Reminder> GetReminderById(string id);
        Task<Reminder> CreateReminder(Reminder reminder);
        Task<Reminder> DeleteReminder(string id);
        Task Save();
    }

    public class ReminderRepository : IReminderRepository
    {
        private readonly ReminderContext _reminderContext;

        public ReminderRepository(ReminderContext reminderContext)
        {
            _reminderContext = reminderContext;
        }

        public async Task<IList<Reminder>> GetReminders()
        {
            return await _reminderContext.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetReminderById(string id)
        {
            return await _reminderContext.Reminders.FirstOrDefaultAsync(r => r.Id.ToString().Equals(id));
        }

        public async Task<Reminder> CreateReminder(Reminder reminder)
        {
            var entry = await _reminderContext.Reminders.AddAsync(reminder);
            return entry.Entity;
        }

        public async Task<Reminder> DeleteReminder(string id)
        {
            var entryToDelete = await GetReminderById(id);
            _reminderContext.Remove(entryToDelete);
            return entryToDelete;
        }

        public async Task Save()
        {
            await _reminderContext.SaveChangesAsync();
        }
    }
}