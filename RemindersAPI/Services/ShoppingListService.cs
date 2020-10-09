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
    public interface IShoppingListService
    {
        Task<IList<ShoppingListItemDTO>> GetShoppingList();
        Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem, string? connectionId);
        Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem, string? connectionId);
        Task<ShoppingListItemDTO> DeleteShoppingListItem(string id, string? connectionId);
    }

    public class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ApplicationHub> _appHubContext;

        public ShoppingListService(IShoppingListRepository shoppingListRepository, IMapper mapper, IHubContext<ApplicationHub> appHubContext)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
            _appHubContext = appHubContext;
        }

        public async Task<IList<ShoppingListItemDTO>> GetShoppingList()
        {
            var shoppingListDTOs = new List<ShoppingListItemDTO>();
            var shoppingList = await _shoppingListRepository.GetShoppingList();
            foreach (var shoppingListItem in shoppingList)
            {
                shoppingListDTOs.Add(_mapper.Map<ShoppingListItem, ShoppingListItemDTO>(shoppingListItem));
            }

            return shoppingListDTOs;
        }

        public async Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem, string? connectionId)
        {
            var shoppingListItemToCreate = _mapper.Map<CreateShoppingListItemCommand, ShoppingListItem>(shoppingListItem);
            var createdShoppingListItem = await _shoppingListRepository.CreateShoppingListItem(shoppingListItemToCreate);
            await _shoppingListRepository.Save();

            var ret = _mapper.Map<ShoppingListItem, ShoppingListItemDTO>(createdShoppingListItem);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.SHOPPING_LIST_ITEM_CREATED, JsonConvert.SerializeObject(ret));

            return ret;
        }

        public async Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem, string? connectionId)
        {
            var shoppingListItemToUpdate = _mapper.Map<ShoppingListItemDTO, ShoppingListItem>(shoppingListItem);
            await _shoppingListRepository.UpdateShoppingListItem(shoppingListItemToUpdate);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.SHOPPING_LIST_ITEM_UPDATED, JsonConvert.SerializeObject(shoppingListItem));
            
            return shoppingListItem;
        }

        public async Task<ShoppingListItemDTO> DeleteShoppingListItem(string id, string? connectionId)
        {
            var deletedShoppingListItem = await _shoppingListRepository.DeleteShoppingListItem(id);
            await _shoppingListRepository.Save();

            var ret = _mapper.Map<ShoppingListItem, ShoppingListItemDTO>(deletedShoppingListItem);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.SHOPPING_LIST_ITEM_DELETED, JsonConvert.SerializeObject(ret));

            return ret;
        }
    }
}
