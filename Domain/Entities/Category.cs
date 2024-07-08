using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public record Category
    {
        private Category(string name, ICollection<Product> products)
        {
            Name = name;
            Products = products;
        }
        private Category(string name)
        {
            Name = name;  
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<Product>? Products { get; private set; }
        public static Category Create(string name, ICollection<Product> products)
        {
            return new Category(name, products);
        }
        public static Category Create(string name)
        {
            // Rise Event unforchnaly i didnt have time to do that
            return new Category(name); ;
        }
        public  Category AddProduct(int id, string name, Product product)
        {
            this.Id = id;
            this.Name = name;
            if(this.Products == null) Products = new HashSet<Product>();
            Products.Add(product);
            return this;
        }
    }
}
