namespace Web_Shop.Application.DTOs
{
    public class GetSingleProductDTO
    {
        public ulong IdProduct { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Sku { get; set; } = string.Empty;
    }
} 