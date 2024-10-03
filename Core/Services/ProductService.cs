
global using Services.Abstractions;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Specifications;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork UnitOfWork ,IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await UnitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsResult = Mapper.Map<IEnumerable<BrandResultDTO>>(brands);
            return brandsResult;
        }

        public async Task<IEnumerable<ProductResultDTO>> GetAllProductsAsync()
        {
            var products = await UnitOfWork.GetRepository<Product,int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications());
            var productResult = Mapper.Map<IEnumerable<ProductResultDTO>>(products);
            return productResult;
        }

        public async Task<IEnumerable<TypeResultDTO>> GetAllTypesAsync()
        {
            var types = await UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typeResult = Mapper.Map<IEnumerable<TypeResultDTO>>(types);
            return typeResult;
        }

        public async Task<ProductResultDTO> GetProductByIdAsync(int id)
        {
            var product = await UnitOfWork.GetRepository<Product, int>().GetAsync(new ProductWithBrandAndTypeSpecifications(id));
            var productResult = Mapper.Map<ProductResultDTO>(product);
            return productResult;
        }
    }
}
