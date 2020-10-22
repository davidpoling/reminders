#nullable enable
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersAPI.SignalR;
using RemindersDomain;
using RemindersDomain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public interface IReminderService
    {
        Task<IList<ReminderDTO>> GetReminders();

        Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder, string? connectionId);

        Task<ReminderDTO> UpdateReminder(ReminderDTO reminder, string? connectionId);

        Task<int> DeleteReminder(int id, string? connectionId);
    }

    public class ReminderService : IReminderService
    {
        private readonly IDbRepository<Reminder> _repo;
        private readonly IMapper _mapper;
        private readonly IHubContext<ApplicationHub> _appHubContext;

        public ReminderService(IDbRepository<Reminder> repo, IMapper mapper, IHubContext<ApplicationHub> appHubContext)
        {
            _repo = repo;
            _mapper = mapper;
            _appHubContext = appHubContext;
        }

        public async Task<IList<ReminderDTO>> GetReminders()
        {
            return await _mapper.ProjectTo<ReminderDTO>(_repo.Search().OrderBy(_ => _.DateTime).ThenByDescending(_ => _.Created)).ToListAsync();
        }

        public async Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder, string? connectionId)
        {
            var reminderToCreate = _mapper.Map<CreateReminderCommand, Reminder>(reminder);
            var createdReminder = await _repo.Create(reminderToCreate);

            var ret = _mapper.Map<Reminder, ReminderDTO>(createdReminder);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.REMINDER_CREATED, JsonConvert.SerializeObject(ret));

            return ret;
        }

        public async Task<ReminderDTO> UpdateReminder(ReminderDTO reminder, string? connectionId)
        {
            var reminderToUpdate = _mapper.Map<ReminderDTO, Reminder>(reminder);
            await _repo.Update(reminderToUpdate);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.REMINDER_UPDATED, JsonConvert.SerializeObject(reminder));

            return reminder;
        }

        public async Task<int> DeleteReminder(int id, string? connectionId)
        {
            id = await _repo.Delete(id);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.REMINDER_DELETED, id);

            return id;
        }
    }
}