
global using Services.Abstractions;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Specifications;
using Shared;

namespace Services
{
    internal class ProductService(IUnitOfWork UnitOfWork, IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDTO>> GetAllBrandsAsync()
        {
            var brands = await UnitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsResult = Mapper.Map<IEnumerable<BrandResultDTO>>(brands);
            return brandsResult;
        }

        public async Task<PaginatedResult<ProductResultDTO>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var products = await UnitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var productResult = Mapper.Map<IEnumerable<ProductResultDTO>>(products);
            var count = productResult.Count();
            var totalCount = await UnitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(parameters));
            var result = new PaginatedResult<ProductResultDTO>
            (parameters.pageIndex,
            count,
            totalCount,
            productResult
            );
            return result;
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
            return product is null ? throw new ProductNotFoundException(id) : Mapper.Map<ProductResultDTO>(product);

        }
    }
}
