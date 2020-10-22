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
    public interface IShoppingListService
    {
        Task<IList<ShoppingListItemDTO>> GetShoppingList();
        Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem, string? connectionId);
        Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem, string? connectionId);
        Task<int> DeleteShoppingListItem(int id, string? connectionId);
    }

    public class ShoppingListService : IShoppingListService
    {
        private readonly IDbRepository<ShoppingListItem> _repo;
        private readonly IMapper _mapper;
        private readonly IHubContext<ApplicationHub> _appHubContext;

        public ShoppingListService(IDbRepository<ShoppingListItem> repo, IMapper mapper, IHubContext<ApplicationHub> appHubContext)
        {
            _repo = repo;
            _mapper = mapper;
            _appHubContext = appHubContext;
        }

        public async Task<IList<ShoppingListItemDTO>> GetShoppingList()
        {
            return await _mapper.ProjectTo<ShoppingListItemDTO>(_repo.Search().OrderByDescending(_ => _.Created)).ToListAsync();
        }

        public async Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem, string? connectionId)
        {
            var shoppingListItemToCreate = _mapper.Map<CreateShoppingListItemCommand, ShoppingListItem>(shoppingListItem);
            var createdShoppingListItem = await _repo.Create(shoppingListItemToCreate);

            var ret = _mapper.Map<ShoppingListItem, ShoppingListItemDTO>(createdShoppingListItem);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.SHOPPING_LIST_ITEM_CREATED, JsonConvert.SerializeObject(ret));

            return ret;
        }

        public async Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem, string? connectionId)
        {
            var shoppingListItemToUpdate = _mapper.Map<ShoppingListItemDTO, ShoppingListItem>(shoppingListItem);
            await _repo.Update(shoppingListItemToUpdate);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.SHOPPING_LIST_ITEM_UPDATED, JsonConvert.SerializeObject(shoppingListItem));
            
            return shoppingListItem;
        }

        public async Task<int> DeleteShoppingListItem(int id, string? connectionId)
        {
            id = await _repo.Delete(id);
            await _appHubContext.Clients.AllExcept(connectionId).SendAsync(MessageConstants.SHOPPING_LIST_ITEM_DELETED, id);

            return id;
        }
    }
}
