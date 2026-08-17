using Microsoft.AspNetCore.Mvc;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;

namespace StoreManager.PL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoryController(IGenericService<CategoryModel> categoryService) : ControllerBase
    {
        private readonly IGenericService<CategoryModel> _categoryService = categoryService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categorys = await _categoryService.GetAllAsync(cancellationToken);
            return Ok(categorys);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryModel>> AddAsync([FromBody] CategoryModel model, CancellationToken cancellationToken = default)
        {
            var created = await _categoryService.AddAsync(model, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id != id)
            {
                return BadRequest($"Route id and body id must match");
            }
            var updated = await _categoryService.UpdateAsync(model, cancellationToken);
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
            var deleted = await _categoryService.RemoveAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}