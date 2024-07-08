
namespace Domain.Entities
{
    public record Product
    {
   
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public static Product Create(string name, int categoryId)
        {
            return new Product(name, categoryId);
        }
        public static Product Create(string name, int categoryId, int ProductId)
        {
            return new Product(name, categoryId, ProductId);
        }
        public static Product Create(string name, Category category)
        {
            return new Product(name, category);
        }
        private Product(string name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }
        private Product(string name, Category category)
        {
            Name = name;
            Category = category;
        }
        public Product Update(string name, Category category)
        {
            this.CategoryId = Category.Id;
            this.Name = name;
            this.Category = category;
            return this;

        }

        private Product(string name, int categoryId, int productId)
        {
            this.Name = name;
            this.Id = productId;
            this.CategoryId = categoryId;
        }
    }
}