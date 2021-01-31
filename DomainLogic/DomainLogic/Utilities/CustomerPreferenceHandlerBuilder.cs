using DomainLogic.DomainLogic.Interfaces;
using DomainLogic.DomainModels;

namespace DomainLogic.DomainLogic.Utilities
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