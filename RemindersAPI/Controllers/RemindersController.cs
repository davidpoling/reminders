using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RemindersAPI.DTOs;
using RemindersAPI.Services;

namespace RemindersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly RemindersService _remindersService;

        public RemindersController(RemindersService remindersService)
        {
            _remindersService = remindersService;
        }

        /// <summary>
        /// Get a list of all reminders
        /// </summary>
        /// <returns>A status along with the list of Reminders.</returns>
        [HttpGet]
        public ActionResult<IList<ReminderDTO>> GetReminders()
        {
            return null;
        }

        /// <summary>
        /// Create a new Reminder
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns>A status along with the newly added Reminder.</returns>
        [HttpPost]
        public ActionResult<ReminderDTO> CreateReminder([FromBody] ReminderDTO reminder)
        {
            return null;
        }

        /// <summary>
        /// Delete a reminder, provided the ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A status along with the deleted Reminder's ID.</returns>
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteReminder(string id)
        {
            return null;
        }
    }
}
