namespace Ecommerce.System.Core.Models
{
    public class Product
    {
        public Guid Id { get; set; } // Globalna unikalność
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime CreateDate { get; set; }
        public List<ProductVariant> Variants { get; set; } // Relacja 1:W
    }
}