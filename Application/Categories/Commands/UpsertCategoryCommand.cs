using Application.Categories.Dto;
using Application.Category.Service;
using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public class UpsertCategoryCommand : IRequest<Response<Domain.Entities.Category>>
    {
        public CategoryDto CategoryDto { get; set; }
        public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, Response<Domain.Entities.Category>>
        {
            private readonly ICategoryService _categoryService;
            public UpsertCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }
            public async Task<Response<Domain.Entities.Category>> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
            {
                //Mapping CategoryDto to Category
                var CategoryUpsert = Domain.Entities.Category.Create(request.CategoryDto.Name, request.CategoryDto.product);
                var result = await _categoryService.UpsertCategoryAsync(CategoryUpsert);
                var response = Response<Domain.Entities.Category>.Create(result, "Custome message");
                return response;

            }
        }
    }
}
