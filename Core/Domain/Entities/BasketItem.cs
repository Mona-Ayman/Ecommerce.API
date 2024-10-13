
namespace Domain.Entities
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductNamme { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
    }
}
