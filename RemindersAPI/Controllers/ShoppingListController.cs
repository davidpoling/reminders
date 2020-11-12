#nullable enable
using Microsoft.AspNetCore.Mvc;
using RemindersAPI.Commands;
using RemindersAPI.DTOs;
using RemindersAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            _shoppingListService = shoppingListService;
        }

        /// <summary>
        /// Get the shopping list.
        /// </summary>
        /// <returns>A status along with the ShoppingList.</returns>
        [HttpGet]
        public async Task<ActionResult<IList<ShoppingListItemDTO>>> GetShoppingList()
        {
            return Ok(await _shoppingListService.GetShoppingList());
        }

        /// <summary>
        /// Create a new ShoppingListItem
        /// </summary>
        /// <param name="shoppingListItem"></param>
        /// <returns>A status along with the newly added ShoppingListItem.</returns>
        [HttpPost]
        public async Task<ActionResult<ShoppingListItemDTO>> CreateShoppingListItem([FromBody] CreateShoppingListItemCommand shoppingListItem)
        {
            return Created(Url.ToString(), await _shoppingListService.CreateShoppingListItem(shoppingListItem));
        }

        /// <summary>
        /// Update a ShoppingListItem
        /// </summary>
        /// <param name="shoppingListItem"></param>
        /// <returns>A status along with the updated ShoppingListItem.</returns>
        [HttpPut]
        public async Task<ActionResult<ShoppingListItemDTO>> UpdateShoppingListItem([FromBody] ShoppingListItemDTO shoppingListItem)
        {
            return Ok(await _shoppingListService.UpdateShoppingListItem(shoppingListItem));
        }

        /// <summary>
        /// Delete a ShoppingListItem, provided the ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A status along with the deleted ShoppingListItem's ID.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteShoppingListItem(int id)
        {
            return Ok(await _shoppingListService.DeleteShoppingListItem(id));
        }
    }
}
