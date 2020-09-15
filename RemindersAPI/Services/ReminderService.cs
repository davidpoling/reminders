using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersDomain;
using RemindersDomain.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public interface IReminderService
    {
        Task<IList<ReminderDTO>> GetReminders();
        Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder);
        Task<ReminderDTO> UpdateReminder(ReminderDTO reminder);
        Task<ReminderDTO> DeleteReminder(string id);
    }

    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task<IList<ReminderDTO>> GetReminders()
        {
            var reminderDTOs = new List<ReminderDTO>();
            var reminders = await _reminderRepository.GetReminders();
            foreach (var reminder in reminders)
            {
                reminderDTOs.Add(new ReminderDTO
                {
                    Id = reminder.Id.ToString(),
                    Text = reminder.Text,
                    DateTime = reminder.DateTime,
                    DateTimeString = reminder.DateTimeString,
                    Complete = reminder.Complete
                });
            }

            return reminderDTOs;
        }

        public async Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder)
        {
            var reminderToCreate = new Reminder
            {
                Text = reminder.Text,
                DateTime = reminder.DateTime,
                DateTimeString = reminder.DateTimeString
            };
            var createdReminder = await _reminderRepository.CreateReminder(reminderToCreate);
            await _reminderRepository.Save();

            return new ReminderDTO
            {
                Id = createdReminder.Id.ToString(),
                Text = createdReminder.Text,
                DateTime = createdReminder.DateTime,
                DateTimeString = createdReminder.DateTimeString
            };
        }

        public async Task<ReminderDTO> UpdateReminder(ReminderDTO reminder)
        {
            var reminderToUpdate = await _reminderRepository.GetReminderById(reminder.Id);
            reminderToUpdate.Text = reminder.Text;
            reminderToUpdate.DateTime = reminder.DateTime;
            reminderToUpdate.DateTimeString = reminder.DateTimeString;
            reminderToUpdate.Complete = reminder.Complete;
            await _reminderRepository.Save();
            return reminder;
        }

        public async Task<ReminderDTO> DeleteReminder(string id)
        {
            var deletedReminder = await _reminderRepository.DeleteReminder(id);
            await _reminderRepository.Save();

            return new ReminderDTO
            {
                Id = deletedReminder.Id.ToString(),
                Text = deletedReminder.Text,
                DateTime = deletedReminder.DateTime,
                DateTimeString = deletedReminder.DateTimeString,
                Complete = deletedReminder.Complete
            };
        }
    }
}
