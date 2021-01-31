using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic.Utilities
{
    public class CustomerPreferenceHandlerBuilder : HandlerBuilder
    {
        public CustomerPreferenceHandlerBuilder WithCustomerPreference(
            CustomerPreferenceCondition customerPreferenceCondition,
            IHandler handler)
        {
            AddHandler(new CustomerPreferenceHandler(
                handler, customerPreferenceCondition));

            return this;
        }
    }
}