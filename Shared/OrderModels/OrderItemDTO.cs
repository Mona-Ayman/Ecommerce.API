﻿
namespace Shared.OrderModels
{
    public record OrderItemDTO
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public string PictureUrl { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }
}
