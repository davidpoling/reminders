using Microsoft.EntityFrameworkCore;
using RemindersDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RemindersDomain
{
    public interface IShoppingListRepository
    {
        Task<IList<ShoppingListItem>> GetShoppingList();
        Task<ShoppingListItem> GetShoppingListItemById(string id);
        Task<ShoppingListItem> CreateShoppingListItem(ShoppingListItem shoppingListItem);
        Task UpdateShoppingListItem(ShoppingListItem shoppingListItem);
        Task<ShoppingListItem> DeleteShoppingListItem(string id);
        Task Save();
    }

    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly ReminderContext _reminderContext;

        public ShoppingListRepository(ReminderContext reminderContext)
        {
            _reminderContext = reminderContext;
        }

        public async Task<IList<ShoppingListItem>> GetShoppingList()
        {
            return await _reminderContext.ShoppingList.ToListAsync();
        }

        public async Task<ShoppingListItem> GetShoppingListItemById(string id)
        {
            return await _reminderContext.ShoppingList.FirstOrDefaultAsync(s => s.Id.ToString().Equals(id));
        }

        public async Task<ShoppingListItem> CreateShoppingListItem(ShoppingListItem shoppingListItem)
        {
            var entry = await _reminderContext.ShoppingList.AddAsync(shoppingListItem);
            return entry.Entity;
        }

        public async Task UpdateShoppingListItem(ShoppingListItem shoppingListItem)
        {
            await _reminderContext.ShoppingList.SingleMergeAsync(shoppingListItem);
        }

        public async Task<ShoppingListItem> DeleteShoppingListItem(string id)
        {
            var entryToDelete = await GetShoppingListItemById(id);
            _reminderContext.Remove(entryToDelete);
            return entryToDelete;
        }

        public async Task Save()
        {
            await _reminderContext.SaveChangesAsync();
        }
    }
}
