using Application.Categories.Dto;
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
    public class GetAllCategoryQuery : IRequest<Response<IEnumerable< Domain.Entities.Category>>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public class UpsertCategoryCommandHandler : IRequestHandler<GetAllCategoryQuery, Response<IEnumerable<Domain.Entities.Category>>>
        {
            private readonly ICategoryService _categoryService;
            public UpsertCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }
            public async Task<Response<IEnumerable<Domain.Entities.Category>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                //Mapping CategoryDto to Category
                var result = await _categoryService.GetAllCategoriesAsync(request. pageNumber ,request. pageSize);
                var response = Response< IEnumerable<Domain.Entities.Category>>.Create(result, "Custome message");
                return response;

            }
        }
    
    }
}
