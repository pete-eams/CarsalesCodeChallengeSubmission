using DomainLogic.DomainLogic.Interfaces;

namespace DomainLogic.DomainModels
{
    public class SalesPersonAssignmentHandler
    {
        private readonly IHandler _handler;
        private readonly CustomerPreferenceCondition _condition;
        public SalesPersonAssignmentHandler(
            CustomerPreferenceCondition condition, 
            IHandler handler)
        {
            _handler = handler;
            _condition = condition;
        }

        public bool MatchesCustomerPreference(CustomerPreference customerPreference)
            => _condition.MatchesCustomerPreference(customerPreference);
            
        public object Handle() 
            => _handler.Handle(null);
    }
}