using Microsoft.AspNetCore.Mvc;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;

namespace StoreManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PaymentController(IGenericService<PaymentModel> paymentService) : ControllerBase
    {
        private readonly IGenericService<PaymentModel> _paymentService = paymentService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var payments = await _paymentService.GetAllAsync(cancellationToken);
            return Ok(payments);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var payment = await _paymentService.GetByIdAsync(id, cancellationToken);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentModel>> AddAsync([FromBody] PaymentModel model, CancellationToken cancellationToken = default)
        {
            var created = await _paymentService.AddAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PaymentModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id and body id: {model} must match");
            }
            var updated = await _paymentService.UpdateAsync(model, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _paymentService.RemoveAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}