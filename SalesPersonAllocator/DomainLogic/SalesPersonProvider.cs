using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    class SalesPersonProvider
    {
        private readonly IHandler _allocationHandler;
        
        public SalesPersonProvider(
            IHandler allocationHandler)
        {
            _allocationHandler = allocationHandler;
        }

        public SalesPerson GetSalesPerson()
            => _allocationHandler.Handle() as SalesPerson;
    }
}
