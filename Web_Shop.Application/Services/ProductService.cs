using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System.Net;
using Web_Shop.Application.DTOs;
using Web_Shop.Application.Mappings;
using Web_Shop.Application.Services.Interfaces;
using Web_Shop.Persistence.UOW.Interfaces;
using WWSI_Shop.Persistence.MySQL.Model;

namespace Web_Shop.Application.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(ILogger<Product> logger,
                             ISieveProcessor sieveProcessor,
                             IOptions<SieveOptions> sieveOptions,
                             IUnitOfWork unitOfWork)
            : base(logger, sieveProcessor, sieveOptions, unitOfWork)
        {
        }

        public async Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> CreateNewProductAsync(AddUpdateProductDTO dto)
        {
            try
            {
                if (await _unitOfWork.ProductRepository.SkuExistsAsync(dto.Sku))
                {
                    return (false, default(Product), HttpStatusCode.BadRequest, "SKU: " + dto.Sku + " already exists.");
                }

                var newEntity = dto.MapProduct();
                var result = await AddAndSaveAsync(newEntity);
                return (true, result.entity, HttpStatusCode.OK, string.Empty);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Product? entity, HttpStatusCode StatusCode, string ErrorMessage)> UpdateExistingProductAsync(AddUpdateProductDTO dto, ulong id)
        {
            try
            {
                var existingEntityResult = await WithoutTracking().GetByIdAsync(id);

                if (!existingEntityResult.IsSuccess)
                {
                    return existingEntityResult;
                }

                if (!await _unitOfWork.ProductRepository.IsSkuEditAllowedAsync(dto.Sku, id))
                {
                    return (false, default(Product), HttpStatusCode.BadRequest, "SKU: " + dto.Sku + " already exists.");
                }

                var domainEntity = dto.MapProduct();
                domainEntity.IdProduct = id;

                return await UpdateAndSaveAsync(domainEntity, id);
            }
            catch (Exception ex)
            {
                return LogError(ex.Message);
            }
        }
    }
} 