using DomainLogic.DomainLogic.Interfaces;

namespace DomainLogic.DomainModels
{
    public class SalesPersonAssignmentHandler
    {
        private readonly IHandler _handler;
        private readonly CustomerPreferenceMatcher _matcher;
        public SalesPersonAssignmentHandler(
            CustomerPreferenceMatcher matcher, 
            IHandler handler)
        {
            _handler = handler;
            _matcher = matcher;
        }

        public bool MatchesCustomerPreference(CustomerPreference customerPreference)
            => _matcher.MatchesCustomerPreference(customerPreference);
            
        public object Handle() 
            => _handler.Handle(null);
    }
}