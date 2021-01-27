using System;
using SalesPersonAllocator.DomainModels;
using System.Collections.Generic;
using SalesPersonAllocator.DomainLogic.Interfaces;

namespace SalesPersonAllocator.DomainLogic
{
    class SalesPersonAllocationProvider
    {
        private readonly Dictionary<CustomerPreference, IHandler> _handlersMap;
        
        public SalesPersonAllocationProvider(SalesPersonMapFactory handlersMapFactory)
        {
            _handlersMap = handlersMapFactory.Create();
        }

        /// <exception cref="UnsupportedCustomerPreferenceException">Thrown if the customer preference requested is unsupported exist.</exception>
        public SalesPerson GetAllocation(
            CustomerPreference preference)
        {
            if (!_handlersMap.TryGetValue(preference, out var handler))
                throw new UnsupportedCustomerPreferenceException();

            return handler.Handle() as SalesPerson;
        }
    }

    public class UnsupportedCustomerPreferenceException : Exception
    {
        public UnsupportedCustomerPreferenceException(string errorMsg = "")
            : base(errorMsg) { }
    }
}
