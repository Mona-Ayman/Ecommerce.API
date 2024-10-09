
global using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        public Task<ProductResultDTO> GetProductByIdAsync(int id);
        public Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        public Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync();
        public Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync();
    }
}
