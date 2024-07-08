using Application.Categories.Commands;
using Application.Categories.Dto;
using Application.Categories.Query;
using Application.Product.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Category>>> GetCategories(int pageNumber = 1, int pageSize = 10)
        {
            var categories = await mediator.Send(new GetAllCategoryQuery() { PageNumber = pageNumber , PageSize = pageSize});
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Entities.Category>> GetCategory(int id)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery() { Id = id });
            if (category.Data == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Entities.Category>> CreateCategory(CategoryDto category)
        {
            var createdCategory = await mediator.Send(new CreateCategoryCommand() { CategoryDto = category });
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Data.Id }, createdCategory);
        }

        [HttpPut]
        public async Task<ActionResult<Domain.Entities.Category>> UpdateCategory(CategoryDto category)
        {
            var updatedCategory = await mediator.Send(new UpdateCategoryCommand() { CategoryDto = category });
            if (updatedCategory.Data == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        [HttpPost("upsert")]
        public async Task<ActionResult<Domain.Entities.Category>> UpsertCategory(CategoryDto category)
        {
            var upsertedCategory = await mediator.Send(new UpsertCategoryCommand() { CategoryDto = category }); 
            return Ok(upsertedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            //var result = await _categoryService.DeleteCategoryAsync(id);
            //if (!result)
            //{
            //    return NotFound();
            //}
            return NoContent();
        }
    }
}
