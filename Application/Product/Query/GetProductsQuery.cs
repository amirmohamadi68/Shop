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

    public class GetProductsQuery : IRequest<Response<IEnumerable<Domain.Entities.Product>>>
    {
        public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<IEnumerable<Domain.Entities.Product>>>
        {
            private readonly IProductService _productService;
            public GetProductsQueryHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<Response<IEnumerable<Domain.Entities.Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                //Mapping CategoryDto to Category
                var result = await _productService.GetProductsAsync();
                var response = Response<IEnumerable<Domain.Entities.Product>>.Create(result, "Custome message");
                return response;

            }
        }

    }
}
