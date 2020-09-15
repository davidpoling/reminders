using AutoMapper;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersDomain.models;

namespace RemindersAPI
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Reminder, ReminderDTO>();

            CreateMap<CreateReminderCommand, Reminder>();

            CreateMap<ReminderDTO, Reminder>();
        }
    }
}
