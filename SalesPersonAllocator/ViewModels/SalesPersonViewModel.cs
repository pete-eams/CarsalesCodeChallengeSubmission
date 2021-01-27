using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.ViewModels
{
    public class SalesPersonViewModel
    {
        private SalesPersonViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public static SalesPersonViewModel FromDomainEntity(
            SalesPerson domainSalesPerson)
            => new SalesPersonViewModel(domainSalesPerson.Name);
    }
}
