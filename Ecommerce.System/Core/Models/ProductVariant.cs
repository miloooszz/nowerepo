namespace Ecommerce.System.Core.Models
{
    public class ProductVariant
    {
        public Guid Id { get; set; } // Zgodnie z założeniem o GUID
        public Guid ProductId { get; set; } // Klucz obcy dla PostgreSQL
        public string Ean { get; set; }
        public decimal Price { get; set; }
        public int StockStatus { get; set; }

        // Relacja 1:W - wariant ma wiele atrybutów
        public List<VariantAttribute> Attributes { get; set; } = new();
    }
}