using Microsoft.AspNetCore.Mvc;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;

namespace StoreManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderItemController(IOrderItemService orderItemService) : ControllerBase
    {
        private readonly IOrderItemService _orderItemService = orderItemService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderItemModel>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var orderItems = await _orderItemService.GetAllAsync(cancellationToken);
            return Ok(orderItems);
        }

        [HttpGet("{productId:int}/{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderItemModel>> GetByIdAsync(int productId, int orderId, CancellationToken cancellationToken = default)
        {
            var orderItem = await _orderItemService.GetByIdAsync(productId, orderId, cancellationToken);
            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderItemModel>> AddAsync([FromBody] OrderItemModel model, CancellationToken cancellationToken = default)
        {
            var created = await _orderItemService.AddAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { productId = created.ProductId, orderId = created.OrderId }, created);
        }

        [HttpPut("{productId:int}/{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int productId, int orderId, [FromBody] OrderItemModel model, CancellationToken cancellationToken = default)
        {
            if (model.ProductId != productId || model.OrderId != orderId)
            {
                return BadRequest($"Route keys (productId, orderId) and body keys must match");
            }
            var updated = await _orderItemService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{productId:int}/{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAsync(int productId, int orderId, CancellationToken cancellationToken = default)
        {
            var deleted = await _orderItemService.RemoveAsync(productId, orderId, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}