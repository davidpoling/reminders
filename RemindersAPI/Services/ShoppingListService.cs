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
    public interface IShoppingListService
    {
        Task<IList<ShoppingListItemDTO>> GetShoppingList();
        Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem);
        Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem);
        Task<int> DeleteShoppingListItem(int id);
    }

    public class ShoppingListService : IShoppingListService
    {
        private readonly IDbRepository<ShoppingListItem> _repo;
        private readonly IMapper _mapper;

        public ShoppingListService(IDbRepository<ShoppingListItem> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IList<ShoppingListItemDTO>> GetShoppingList()
        {
            return await _mapper.ProjectTo<ShoppingListItemDTO>(_repo.Search().OrderByDescending(_ => _.Created)).ToListAsync();
        }

        public async Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem)
        {
            var shoppingListItemToCreate = _mapper.Map<CreateShoppingListItemCommand, ShoppingListItem>(shoppingListItem);
            var createdShoppingListItem = await _repo.Create(shoppingListItemToCreate);

            var ret = _mapper.Map<ShoppingListItem, ShoppingListItemDTO>(createdShoppingListItem);

            return ret;
        }

        public async Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem)
        {
            var shoppingListItemToUpdate = _mapper.Map<ShoppingListItemDTO, ShoppingListItem>(shoppingListItem);
            await _repo.Update(shoppingListItemToUpdate);

            return shoppingListItem;
        }

        public async Task<int> DeleteShoppingListItem(int id)
        {
            id = await _repo.Delete(id);

            return id;
        }
    }
}
