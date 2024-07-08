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

    public class CreateProductCommand : IRequest<Response<Domain.Entities.Product>>
    {
        public ProductDto ProductDTO { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Domain.Entities.Product>>
        {
            private readonly ICategoryService _categoryService;
            private readonly IProductService _productService;
            public CreateProductCommandHandler(ICategoryService categoryService, IProductService productService)
            {
                _categoryService = categoryService;
                _productService = productService;
            }
            public async Task<Response<Domain.Entities.Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var category = await _categoryService.GetCategoryByNameAsync(request.ProductDTO.CategoryName);
                if (category == null)
                {
                    // this custom logic , if user send product that there wasnt in db then AVOID TO CREate Product
                    return Response<Domain.Entities.Product>.Create(null, "Not Found"); 
                }
                var product = Domain.Entities.Product.Create(request.ProductDTO.Name , category);
                product.Update(request.ProductDTO.Name,category);
                var result = await _productService.CreateProductAsync(product);
                var response = Domain.Core.Response<Domain.Entities.Product>.Create(result, "Custome message");
                return response;

            }
        }
    }
}
