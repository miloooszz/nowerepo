namespace Ecommerce.System.Core.Models
{
    public class VariantAttribute
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public string Key { get; set; } // np. "Kolor"
        public string Value { get; set; } // np. "Czarny"
    }
}