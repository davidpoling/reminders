#nullable enable
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersAPI.SignalR;
using RemindersDomain;
using RemindersDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public interface IReminderService
    {
        Task<IList<ReminderDTO>> GetReminders();
        Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder, string? connectionId);
        Task<ReminderDTO> UpdateReminder(ReminderDTO reminder, string? connectionId);
        Task<ReminderDTO> DeleteReminder(string id, string? connectionId);
    }

    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ApplicationHub> _appHubContext;

        public ReminderService(IReminderRepository reminderRepository, IMapper mapper, IHubContext<ApplicationHub> appHubContext)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
            _appHubContext = appHubContext;
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

        public async Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder, string? connectionId)
        {
            var reminderToCreate = _mapper.Map<CreateReminderCommand, Reminder>(reminder);
            var createdReminder = await _reminderRepository.CreateReminder(reminderToCreate);
            await _reminderRepository.Save();

            var ret = _mapper.Map<Reminder, ReminderDTO>(createdReminder);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.REMINDER_CREATED, JsonConvert.SerializeObject(ret));

            return ret;
        }

        public async Task<ReminderDTO> UpdateReminder(ReminderDTO reminder, string? connectionId)
        {
            var reminderToUpdate = _mapper.Map<ReminderDTO, Reminder>(reminder);
            await _reminderRepository.UpdateReminder(reminderToUpdate);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.REMINDER_UPDATED, JsonConvert.SerializeObject(reminder));
            
            return reminder;
        }

        public async Task<ReminderDTO> DeleteReminder(string id, string? connectionId)
        {
            var deletedReminder = await _reminderRepository.DeleteReminder(id);
            await _reminderRepository.Save();

            return _mapper.Map<Reminder, ReminderDTO>(deletedReminder);
        }
    }
}
