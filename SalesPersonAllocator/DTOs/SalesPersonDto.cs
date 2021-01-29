using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DTOs
{
    public class SalesPersonDto
    {
        private SalesPersonDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public static SalesPersonDto FromDomainEntity(
            SalesPerson domainSalesPerson)
            => new SalesPersonDto(domainSalesPerson.Name);
    }
}
