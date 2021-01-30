using System;
using SalesPersonAllocator.DomainModels;
using System.Collections.Generic;
using System.Linq;
using SalesPersonAllocator.DomainLogic.Interfaces;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPersonAllocationProvider
    {
        private readonly List<SalesPersonAssignmentHandler> _assignmentHandlers;

        public SalesPersonAllocationProvider(SalesPersonMapFactory handlersMapFactory)
        {
            _assignmentHandlers = handlersMapFactory.Create();
        }

        /// <exception cref="UnsupportedCustomerPreferenceException">Thrown if the customer preference requested is unsupported exist.</exception>
        public AllocatableSalesPerson GetAllocation(
            CustomerPreference preference)
        {
            var assignmentHandler = _assignmentHandlers
                    .FirstOrDefault(r 
                        => r.MatchesCustomerPreference(preference));
            
            if (assignmentHandler == null)
                throw new UnsupportedCustomerPreferenceException();

            var result = assignmentHandler.Handle();
            return result as AllocatableSalesPerson;
        }
    }

    public class UnsupportedCustomerPreferenceException : Exception
    {
        public UnsupportedCustomerPreferenceException(string errorMsg = "")
            : base(errorMsg) { }
    }
}
