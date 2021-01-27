using System;
using SalesPersonAllocator.DomainModels;
using System.Collections.Generic;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPersonAllocationProvider
    {
        private readonly Dictionary<CustomerPreference, SalesPersonProvider> _handlersMap;
        
        public SalesPersonAllocationProvider()
        {
            _handlersMap = new Dictionary<CustomerPreference, SalesPersonProvider>();
        }

        /// <exception cref="UnsupportedCustomerPreferenceException">Thrown if the customer preference requested is unsupported exist.</exception>
        public SalesPerson GetAllocation(
            CustomerPreference preference)
        {
            if (!_handlersMap.TryGetValue(preference, out var handler))
                throw new UnsupportedCustomerPreferenceException();

            return handler.GetSalesPerson();
        }
    }

    public class UnsupportedCustomerPreferenceException : Exception
    {
        public UnsupportedCustomerPreferenceException(string errorMsg = "")
            : base(errorMsg) { }
    }
}
