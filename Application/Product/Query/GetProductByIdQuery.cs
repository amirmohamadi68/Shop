using Application.Product.Service;
using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Query
{

    public class GetProductByIdQuery : IRequest<Response<Domain.Entities.Product>>
    {
        public int ProductId { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Domain.Entities.Product>>
        {
            private readonly IProductService _productService;
            public GetProductByIdQueryHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<Response<Domain.Entities.Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                //Mapping CategoryDto to Category
                var result = await _productService.GetProductByIdAsync(request.ProductId);
                var response = Response<Domain.Entities.Product>.Create(result, "Custome message");
                return response;

            }
        }

    }
}
