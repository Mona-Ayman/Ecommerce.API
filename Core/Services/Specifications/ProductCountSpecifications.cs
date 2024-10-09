
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services.Specifications
{
    internal class ProductCountSpecifications : Specifications<Product>
    {
        public ProductCountSpecifications(ProductSpecificationParameters parameters)
            : base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId)
            && (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId)
            && (string.IsNullOrWhiteSpace(parameters.search) || product.Name.ToLower().Contains(parameters.search.ToLower().Trim()))
            )
        {
        }


    }
}
