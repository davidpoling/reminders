#nullable enable
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
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
        Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder);
        Task<ReminderDTO> UpdateReminder(ReminderDTO reminder);
        Task<int> DeleteReminder(int id);
        Task<int> DeleteCompletedReminders();
    }

    public class ReminderService : IReminderService
    {
        private readonly IDbRepository<Reminder> _repo;
        private readonly IMapper _mapper;

        public ReminderService(IDbRepository<Reminder> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ReminderDTO>> GetReminders()
        {
            return await _mapper.ProjectTo<ReminderDTO>(_repo.Search().OrderBy(_ => _.DateTime).ThenByDescending(_ => _.Created)).ToListAsync();
        }

        public async Task<ReminderDTO> CreateReminder(CreateReminderCommand reminder)
        {
            var reminderToCreate = _mapper.Map<CreateReminderCommand, Reminder>(reminder);
            var createdReminder = await _repo.Create(reminderToCreate);

            var ret = _mapper.Map<Reminder, ReminderDTO>(createdReminder);

            return ret;
        }

        public async Task<ReminderDTO> UpdateReminder(ReminderDTO reminder)
        {
            var reminderToUpdate = _mapper.Map<ReminderDTO, Reminder>(reminder);
            await _repo.Update(reminderToUpdate);

            return reminder;
        }

        public async Task<int> DeleteReminder(int id)
        {
            id = await _repo.Delete(id);

            return id;
        }

        public async Task<int> DeleteCompletedReminders()
        {
            return await _repo.DeleteCompleted();
        }
    }
}