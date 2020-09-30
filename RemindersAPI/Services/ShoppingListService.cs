using AutoMapper;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersDomain;
using RemindersDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindersAPI.Services
{
    public interface IShoppingListService
    {
        Task<IList<ShoppingListItemDTO>> GetShoppingList();
        Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem);
        Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem);
        Task<ShoppingListItemDTO> DeleteShoppingListItem(string id);
    }

    public class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;

        public ShoppingListService(IShoppingListRepository shoppingListRepository, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
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

        public async Task<ShoppingListItemDTO> CreateShoppingListItem(CreateShoppingListItemCommand shoppingListItem)
        {
            var shoppingListItemToCreate = _mapper.Map<CreateShoppingListItemCommand, ShoppingListItem>(shoppingListItem);
            var createdShoppingListItem = await _shoppingListRepository.CreateShoppingListItem(shoppingListItemToCreate);
            await _shoppingListRepository.Save();

            return _mapper.Map<ShoppingListItem, ShoppingListItemDTO>(createdShoppingListItem);
        }

        public async Task<ShoppingListItemDTO> UpdateShoppingListItem(ShoppingListItemDTO shoppingListItem)
        {
            var shoppingListItemToUpdate = _mapper.Map<ShoppingListItemDTO, ShoppingListItem>(shoppingListItem);
            await _shoppingListRepository.UpdateShoppingListItem(shoppingListItemToUpdate);
            return shoppingListItem;
        }

        public async Task<ShoppingListItemDTO> DeleteShoppingListItem(string id)
        {
            var deletedShoppingListItem = await _shoppingListRepository.DeleteShoppingListItem(id);
            await _shoppingListRepository.Save();

            return _mapper.Map<ShoppingListItem, ShoppingListItemDTO>(deletedShoppingListItem);
        }
    }
}
