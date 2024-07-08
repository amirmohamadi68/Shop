using Application.Product.Commands;
using Application.Product.Dto;
using Application.Product.Query;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int Id)
        {
            var result = await mediator.Send(new GetProductsQuery() );
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await mediator.Send(new GetProductsQuery());
        
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateProduct( ProductDto productDto)
        {
            var result = await mediator.Send(new CreateProductCommand {ProductDTO =  productDto});
            return Ok();
   
            return CreatedAtAction(nameof(GetProductById), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            try
            {
                var updatedProduct = await mediator.Send(new UpdateProductCommand { productDto = productDto });
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // i havnt time to create this sorry
            //await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
