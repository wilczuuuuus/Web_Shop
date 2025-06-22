using Microsoft.EntityFrameworkCore;
using Web_Shop.Persistence.Repositories.Interfaces;
using WWSI_Shop.Persistence.MySQL.Model;

namespace Web_Shop.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _context.Set<Product>().AnyAsync(p => p.Sku == sku);
        }

        public async Task<bool> IsSkuEditAllowedAsync(string sku, ulong productId)
        {
            return !await _context.Set<Product>().AnyAsync(p => p.Sku == sku && p.IdProduct != productId);
        }
    }
} 