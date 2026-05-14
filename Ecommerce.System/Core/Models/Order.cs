namespace Ecommerce.System.Core.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateoftheOrder { get; set; }
        public string Status { get; set; }
        public decimal TotalValue { get; set; } 
        public string OrderAddress { get; set; }


        public List<OrderStatus> OrderItems { get; set; } = new List<OrderStatus>();
    }
}