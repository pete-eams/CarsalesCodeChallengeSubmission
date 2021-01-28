using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SalesPersonAllocatorTest")]
namespace SalesPersonAllocator.DomainModels
{
    class InvalidSalesPerson : SalesPerson
    {
        public InvalidSalesPerson() : base("")
        {
        }
    }
}
