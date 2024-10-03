
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        //CTOR is used to get product by id 
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(product => product.Id == id)
        {
            AddIncludes(product => product.ProductBrand);
            AddIncludes(product => product.ProductType);
        }

        //CTOR is used to get all products
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddIncludes(product => product.ProductBrand);
            AddIncludes(product => product.ProductType);
        }

    }
}
