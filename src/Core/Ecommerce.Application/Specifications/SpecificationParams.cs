namespace Ecommerce.Application.Specifications
{
    public class SpecificationParams
    {
        public string? Sort { get; set; }
        public int PageIndex { get; set; }
        private const int MaxpageSize = 50;
        private int _pageSize = 3;

        public int PageSize { 
        
            get => _pageSize;
            set => _pageSize = (value > MaxpageSize) ? MaxpageSize : value;
        }

        public string? Search { get; set; }
    }
}
