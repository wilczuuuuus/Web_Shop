using WWSI_Shop.Persistence.MySQL.Model;

namespace Web_Shop.Persistence.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> SkuExistsAsync(string sku);
        Task<bool> IsSkuEditAllowedAsync(string sku, ulong productId);
    }
} 