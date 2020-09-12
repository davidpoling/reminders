﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemindersAPI.DTOs;
using RemindersAPI.Services;

namespace RemindersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly IRemindersService _remindersService;

        public RemindersController(IRemindersService remindersService)
        {
            _remindersService = remindersService;
        }

        /// <summary>
        /// Get a list of all reminders
        /// </summary>
        /// <returns>A status along with the list of Reminders.</returns>
        [HttpGet]
        public async Task<ActionResult<IList<ReminderDTO>>> GetReminders()
        {
            return Ok(await _remindersService.GetReminders());
        }

        /// <summary>
        /// Create a new Reminder
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns>A status along with the newly added Reminder.</returns>
        [HttpPost]
        public async Task<ActionResult<ReminderDTO>> CreateReminder([FromBody] ReminderDTO reminder)
        {
            return Ok(await _remindersService.CreateReminder(reminder));
        }

        /// <summary>
        /// Delete a reminder, provided the ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A status along with the deleted Reminder's ID.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteReminder(string id)
        {
            return Ok(await _remindersService.DeleteReminder(id));
        }
    }
}
