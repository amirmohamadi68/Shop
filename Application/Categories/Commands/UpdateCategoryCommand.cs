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
    public class UpdateCategoryCommand : IRequest<Response<Domain.Entities.Category>>
    {
        public CategoryDto CategoryDto { get; set; }
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<Domain.Entities.Category>>
        {
            private readonly ICategoryService _categoryService;
            public UpdateCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;           
            }
            public async Task<Response<Domain.Entities.Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                //Mapping CategoryDto to Category
                var CategoryUpdatedValue = Domain.Entities.Category.Create(request.CategoryDto.Name, request.CategoryDto.product);
              var result = await _categoryService.UpdateCategoryAsync(CategoryUpdatedValue);
                var response = Response<Domain.Entities.Category>.Create(result, "Custome message");
                return response;

            }
        }
    }
}
