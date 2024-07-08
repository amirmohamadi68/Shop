using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Dto
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public ICollection<Domain.Entities.Product>? product {get ; set; }
    }
}
