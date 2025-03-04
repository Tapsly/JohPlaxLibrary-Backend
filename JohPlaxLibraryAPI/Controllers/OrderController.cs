/*
 This file contains code that access the order service through constructor
 It defines the http methods used to receive data from the client request,
 process the request and generates responses that are sent back to the client. 
 */
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
        public async Task<IActionResult<IEnumerable<Order>>> GetOrdersAsync()
            => Ok(await _ordersService.GetOrdersAsync());

        [HttpGet("{bookId:length(24)}")]
        public async Task<IActionResult<IEnumerable<Order>>> GetOrdersByBookIdAsync([FromRoute] string bookId)
        {
            try
            {
                Ok(await _ordersService.GetOrdersByBookIdAsync());
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("{userId:length(24)}")]
        public async Task<IActionResult<IEnumerable<Order>>> GetOrdersByUserIdAsync([FromRoute] string userId)
        {
            try
            {
                Ok(await _ordersService.GetOrdersByUserIdAsync);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult<Order>> GetOrderByIdAsync([FromRoute] string id)
        {
            try
            {
                var existingOrder = await _ordersService.GetOrderByIdAsync();

                return existingOrder is null ? NotFound() : Ok(existingOrder);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet("{userId:length(24)}")]
        public async Task<IActionResult<Order>> GetOrderByUserIdAsync([FromRoute] string userId)
        {
            try
            {
                var existingOrder = await _ordersService.GetOrderByUserIdAsync();

                return existingOrder is null ? NotFound() : Ok(existingOrder);
            }
            catch (Exception e)
            {

                throw e;
            }
         
        }

        [HttpGet("{bookId:length(24)}")]
        public async Task<IActionResult<Order>> GetOrderByUserIdAsync([FromRoute] string bookId)
        {
            try
            {
                var existingOrder = await _ordersService.GetOrderByBookIdAsync();

                return existingOrder is null ? NotFound() : Ok(existingOrder);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync([FromBody] Order order)
        {
            var createdOrder = await _ordersService.CreateOrderAsync(order);

            return createdOrder is null ? throw new Exception("Failed to create Order") :
                CreatedAtAction(nameof(GetOrderByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateOrderByIdAsync([FromRoute]string id,[FromBody] Order updatedOrder)
        {
            try
            {
                var queryOrder = await _ordersService.GetOrderByIdAsync(id);

                if (queryOrder is null)
                {
                    return NotFound();
                }

                await _ordersService.UpdateOrderByIdAsync(id, updatedOrder);

                return NoContent();
            }
            catch (Exception e)
            {

                throw e;
            }
          
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteOrderByIdAsync([FromRoute] string id)
        {
            try
            {
                var existingOrder = await _ordersService.GetOrderByIdAsync(id);

                if (existingOrder is null)
                {
                    return NotFound();
                }

                await _ordersService.DeleteOrderByIdAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        [HttpDelete("{bookId:length(24)}")]
        public async Task<IActionResult> DeleteOrderByBookIdAsync([FromRoute] string bookId)
        {
            try
            {
                var existingOrder = await _ordersService.DeleteOrdersByBookIdAsync(bookId);

                if (existingOrder is null)
                {
                    return NotFound();
                }

                await _ordersService.DeleteOrdersByBookIdAsync(bookId);
                return NoContent();
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
