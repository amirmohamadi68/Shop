using Application.Category.Service;
using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Query
{

    public class GetCategoryByIdQuery : IRequest<Response<Domain.Entities.Category>>
    {
     public int Id { get; set; }
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<Domain.Entities.Category>>
        {
            private readonly ICategoryService _categoryService;
            public GetCategoryByIdQueryHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }
            public async Task<Response<Domain.Entities.Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                //Mapping CategoryDto to Category
                var result = await _categoryService.GetCategoryByIdAsync(request.Id);
                var response = Response<Domain.Entities.Category>.Create(result, "Custome message");
                return response;

            }
        }

    }
}
