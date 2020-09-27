using AutoMapper;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersDomain;
using RemindersDomain.models;
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
        private readonly IMapper _mapper;

        public ReminderService(IReminderRepository reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }

        public async Task<IList<ReminderDTO>> GetReminders()
        {
            var reminderDTOs = new List<ReminderDTO>();
            var reminders = await _reminderRepository.GetReminders();
            foreach (var reminder in reminders)
            {
                reminderDTOs.Add(_mapper.Map<Reminder, ReminderDTO>(reminder));
            }

            return reminderDTOs;
        }

        public async Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder)
        {
            var reminderToCreate = _mapper.Map<CreateReminderCommand, Reminder>(reminder);
            var createdReminder = await _reminderRepository.CreateReminder(reminderToCreate);
            await _reminderRepository.Save();

            return _mapper.Map<Reminder, ReminderDTO>(createdReminder);
        }

        public async Task<ReminderDTO> UpdateReminder(ReminderDTO reminder)
        {
            var reminderToUpdate = _mapper.Map<ReminderDTO, Reminder>(reminder);
            await _reminderRepository.UpdateReminder(reminderToUpdate);
            return reminder;
        }

        public async Task<ReminderDTO> DeleteReminder(string id)
        {
            var deletedReminder = await _reminderRepository.DeleteReminder(id);
            await _reminderRepository.Save();

            return _mapper.Map<Reminder, ReminderDTO>(deletedReminder);
        }
    }
}
