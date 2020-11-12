#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersAPI.Services;

namespace RemindersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _remindersService;

        public ReminderController(IReminderService remindersService)
        {
            _remindersService = remindersService;
        }

        /// <summary>
        /// Get a list of all reminders.
        /// </summary>
        /// <returns>A status along with the list of Reminders.</returns>
        [HttpGet]
        public async Task<ActionResult<IList<ReminderDTO>>> GetReminders()
        {
            return Ok(await _remindersService.GetReminders());
        }

        /// <summary>
        /// Create a new Reminder.
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns>A status along with the newly added Reminder.</returns>
        [HttpPost]
        public async Task<ActionResult<ReminderDTO>> CreateReminder([FromBody] CreateReminderCommand reminder)
        {
            return Created(Url.ToString(), await _remindersService.CreateReminder(reminder));
        }

        /// <summary>
        /// Update a Reminder.
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns>A status along with the updated Reminder.</returns>
        [HttpPut]
        public async Task<ActionResult<ReminderDTO>> UpdateReminder([FromBody] ReminderDTO reminder)
        {
            return Ok(await _remindersService.UpdateReminder(reminder));
        }

        /// <summary>
        /// Delete a Reminder, provided the ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A status along with the deleted Reminder's ID.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteReminder(int id)
        {
            return Ok(await _remindersService.DeleteReminder(id));
        }
    }
}
