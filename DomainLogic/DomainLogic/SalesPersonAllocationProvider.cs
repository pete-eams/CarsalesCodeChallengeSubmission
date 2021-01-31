using DomainLogic.DomainLogic.Interfaces;
using DomainLogic.DomainModels;

namespace DomainLogic.DomainLogic
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
