
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record BasketItemDTO
    {
        public string Id { get; init; }
        public string ProductNamme { get; init; }
        public string PictureUrl { get; init; }
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }
        public string Category { get; init; }
        public string Brand { get; init; }
        [Range(1, 99)]
        public int Quantity { get; init; }
    }
}
