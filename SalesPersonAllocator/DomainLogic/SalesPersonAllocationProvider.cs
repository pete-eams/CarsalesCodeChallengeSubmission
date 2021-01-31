using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.DomainLogic.Interfaces;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPersonAllocationProvider
    {
        private readonly IHandler _customerPreferenceHandler;

        public SalesPersonAllocationProvider(SalesPersonMapFactory handlersMapFactory)
        {
            _customerPreferenceHandler = handlersMapFactory.Create();
        }
        
        public AllocatableSalesPerson GetAllocation(
            CustomerPreference preference)
            => _customerPreferenceHandler.Handle(preference) 
                as AllocatableSalesPerson;
    }
}
