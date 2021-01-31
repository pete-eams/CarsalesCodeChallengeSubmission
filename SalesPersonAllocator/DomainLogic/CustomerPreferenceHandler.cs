using System;
using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    public class CustomerPreferenceHandler : IHandler
    {
        private IHandler _nextHandler;
        private readonly IHandler _allocationRuleHandler;
        private readonly CustomerPreferenceCondition _customerPreferenceCondition;

        public CustomerPreferenceHandler(
            IHandler handler,
            CustomerPreferenceCondition customerPreferenceCondition)
        {
            _allocationRuleHandler = handler;
            _customerPreferenceCondition = customerPreferenceCondition;
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

            return _customerPreferenceCondition.MatchesCustomerPreference(customerPreference) 
                ? _allocationRuleHandler.Handle(null) : _nextHandler?.Handle(request);
        }
    }
}
