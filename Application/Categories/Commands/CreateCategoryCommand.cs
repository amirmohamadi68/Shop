using Application.Categories.Dto;
using Application.Category.Service;
using Domain.Core;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<Response<Domain.Entities.Category>>
    {
        public CategoryDto CategoryDto { get; set; }
        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<Domain.Entities.Category>>
        {
            private readonly ICategoryService _categoryService;
            public CreateCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }
            public async Task<Response<Domain.Entities.Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = Domain.Entities.Category.Create(request.CategoryDto.Name);

             var result = await _categoryService.CreateCategoryAsync(category);
                var response = Domain.Core.Response<Domain.Entities.Category>.Create(result, "Custome message");
                return response;

            }
        }
    }
}
