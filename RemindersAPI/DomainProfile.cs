using AutoMapper;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersDomain.Models;

namespace RemindersAPI
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // Reminders
            CreateMap<Reminder, ReminderDTO>();

            CreateMap<CreateReminderCommand, Reminder>();

            CreateMap<ReminderDTO, Reminder>();

            // Shopping List
            CreateMap<ShoppingListItem, ShoppingListItemDTO>();

            CreateMap<CreateShoppingListItemCommand, ShoppingListItem>();

            CreateMap<ShoppingListItemDTO, ShoppingListItem>();
        }
    }
}
