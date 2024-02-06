using Domain;

namespace PresentationLayer.Models
{
    public class OrderModelDTO
    {
        public int Id { get; set; }
        public OrderItemModelDTO[] Items { get; set; } = new OrderItemModelDTO[0];
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string CellPhone { get; set; }

        public string DeliveryDescription { get; set; }

        public string PaymentDescription { get; set; }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}
