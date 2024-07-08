using Application.Category.Service;
using Application.Product.Dto;
using Application.Product.Service;
using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Commands
{
    public class UpdateProductCommand : IRequest<Response<Domain.Entities.Product>>
    {
        public ProductDto productDto { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Domain.Entities.Product>>
        {
            private readonly ICategoryService _categoryService;
            private readonly IProductService _productService;
            public UpdateProductCommandHandler(ICategoryService categoryService, IProductService productService)
            {
                _categoryService = categoryService;
                _productService = productService;
            }
            public async Task<Response<Domain.Entities.Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var oldProduct = await _productService.GetProductByIdAsync(request.productDto.ProductId.Value);
                var category = await _categoryService.GetCategoryByNameAsync(request.productDto.CategoryName);
                if (category == null) { return Response<Domain.Entities.Product>.Create(null, "Not Found"); }
                var Newproduct = oldProduct.Update(request.productDto.Name, category);

                var result = await _productService.UpdateProductAsync(oldProduct.Id , Newproduct);
                var response = Domain.Core.Response<Domain.Entities.Product>.Create(result, "Custome message");
                return response;

            }
        }
    {
    }
}
