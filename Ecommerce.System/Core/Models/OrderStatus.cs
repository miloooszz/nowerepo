namespace Ecommerce.System.Core.Models
{
    public class OrderStatus
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }

        // Kluczowe dla audytu: cena w chwili zakupu, niezależna od zmian w katalogu
        public decimal Priceatthetimeofpurchase { get; set; }

        // Data zmiany statusu pozycji
        public DateTime ChangedAt { get; set; }

        // Status tej pozycji (np. Zarezerwowano, Wysłano)
        public string Status { get; set; } = string.Empty;
    }
}