
using Domain.Contracts;
using Domain.Entities;
using Shared;

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
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters)
            : base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId)
            && (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId)
            && (string.IsNullOrWhiteSpace(parameters.search) || product.Name.ToLower().Contains(parameters.search.ToLower().Trim()) )
            )                           //                هنا لو براندايدي جايله قيمة يعني ترو ونوت الترو فولس يبقي هيدخل ع الكونديشن التاني انه يجيب البراندايدي بتساوي البراندايدي اللي مبعوت
                                                                                                               //لكن لو كان البراندايدي جاي نال يبقي هاز فاليو ب فولس ونوت الفولس ترو يبقي هيجيب البروداكتس اللي عندها براندايدي  يعني كل البروداكتس وهو ده اللي عاوزاه انه لو متبعتش براندايدي يرجع كل البروداكتس  وهو هنا مش هيكمل الكونديشن عشان انا مستخدمة || ودي شورت سيرقل يعني لو لقي اول جزء بترو مش هيكمل
        {
            AddIncludes(product => product.ProductBrand);
            AddIncludes(product => product.ProductType);

            SetPagination(parameters.pageIndex, parameters.pageSize);

            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortingOptions.PriceAsc:
                        SetOrderBy(product => product.Price);
                        break;
                    case ProductSortingOptions.PriceDesc:
                        SetOrderByDesc(product => product.Price);
                        break;
                    case ProductSortingOptions.NameDesc:
                        SetOrderByDesc(product => product.Name);
                        break;
                    case ProductSortingOptions.NameAsc:
                        SetOrderBy(product => product.Name);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
