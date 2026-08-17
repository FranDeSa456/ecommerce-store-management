using Microsoft.AspNetCore.Mvc;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;

namespace StoreManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReviewController(IGenericService<ReviewModel> reviewService) : ControllerBase
    {
        private readonly IGenericService<ReviewModel> _reviewService = reviewService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var reviews = await _reviewService.GetAllAsync(cancellationToken);
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var review = await _reviewService.GetByIdAsync(id, cancellationToken);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReviewModel>> AddAsync([FromBody] ReviewModel model, CancellationToken cancellationToken = default)
        {
            var created = await _reviewService.AddAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReviewModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id and body id must match");
            }
            var updated = await _reviewService.UpdateAsync(model, cancellationToken);
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
            var deleted = await _reviewService.RemoveAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}