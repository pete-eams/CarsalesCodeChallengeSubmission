using DomainLogic.DomainLogic.Interfaces;
using DomainLogic.DomainModels;

namespace DomainLogic.DomainLogic.Utilities
{
    public class CustomerPreferenceHandlerBuilder : HandlerBuilder
    {
        public CustomerPreferenceHandlerBuilder WithCustomerPreference(
            CustomerPreferenceMatcher customerPreferenceMatcher,
            IHandler handler)
        {
            AddHandler(new CustomerPreferenceHandler(
                handler, customerPreferenceMatcher));

            return this;
        }
    }
}