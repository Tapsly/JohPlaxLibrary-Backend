
using JohPlaxLibraryAPI.Interfaces;
using JohPlaxLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JohPlaxLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrderController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersAsync()
            => Ok(await _ordersService.GetOrdersAsync());

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> GetOrderByIdAsync(string id)
        {
            var existingOrder = await _ordersService.GetOrderByIdAsync(id);

            return existingOrder is null ? NotFound() : Ok(existingOrder);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync(Order order)
        {
            var createdOrder = await _ordersService.CreateOrderAsync(order);

            return createdOrder is null ? throw new Exception("Failed to create Order") :
                CreatedAtAction(nameof(GetOrderByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> UpdateOrderByIdAsync(string id, Order updatedOrder)
        {
            var queryOrder = await _ordersService.GetOrderByIdAsync(id);

            if (queryOrder is null)
            {
                return NotFound();
            }

            await _ordersService.UpdateOrderByIdAsync(id, updatedOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteOrderByIdAsync(string id)
        {
            var existingOrder = await _ordersService.GetOrderByIdAsync(id);

            if (existingOrder is null)
            {
                return NotFound();
            }

            await _ordersService.DeleteOrderByIdAsync(id);
            return NoContent();
        }
    }
}
