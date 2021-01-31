using DomainLogic.DomainModels;

namespace SalesPersonAllocator.DTOs
{
    public class SalesPersonDto
    {
        private SalesPersonDto(string name, bool isAllocated)
        {
            Name = name;
            IsAllocated = isAllocated;
        }

        public string Name { get; set; }
        public bool IsAllocated { get; set; }

        public static SalesPersonDto FromDomainEntity(
            AllocatableSalesPerson domainSalesPerson)
            => new SalesPersonDto(
                domainSalesPerson.Name, 
                domainSalesPerson.IsAllocated);

        public static SalesPersonDto Empty()
            => new SalesPersonDto("", false);
    }
}
