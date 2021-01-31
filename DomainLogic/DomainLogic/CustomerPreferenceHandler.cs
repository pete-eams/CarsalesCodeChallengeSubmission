using System;
using DomainLogic.DomainLogic.Interfaces;
using DomainLogic.DomainModels;

namespace DomainLogic.DomainLogic
{
    public class CustomerPreferenceHandler : IHandler
    {
        private IHandler _nextHandler;
        private readonly IHandler _allocationRuleHandler;
        private readonly CustomerPreferenceMatcher _customerPreferenceMatcher;

        public CustomerPreferenceHandler(
            IHandler handler,
            CustomerPreferenceMatcher customerPreferenceMatcher)
        {
            _allocationRuleHandler = handler;
            _customerPreferenceMatcher = customerPreferenceMatcher;
        }

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return _nextHandler;
        }

        public object Handle(object request)
        {
            if (!(request is CustomerPreference customerPreference))
                throw new InvalidOperationException(
                    $"{GetType()} Does not support the argument type: {request.GetType()}");

            return _customerPreferenceMatcher.MatchesCustomerPreference(customerPreference) 
                ? _allocationRuleHandler.Handle(null) : _nextHandler?.Handle(request);
        }
    }
}
