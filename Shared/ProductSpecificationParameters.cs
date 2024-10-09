
namespace Shared
{
    public class ProductSpecificationParameters
    {
        private const int MAXPAGESIZE = 10;
        private const int DEFAULTPAGESIZE = 5;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions? Sort { get; set; }
        public int pageIndex { get; set; } = 1;
        private int _pageSize = DEFAULTPAGESIZE;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value < DEFAULTPAGESIZE ? DEFAULTPAGESIZE : value;
        }
        public string?  search { get; set; }

    }
    public enum ProductSortingOptions
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc
    }

}
