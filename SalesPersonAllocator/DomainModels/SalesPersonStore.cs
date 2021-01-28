using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesPersonAllocator.DomainModels
{
    public class SalesPersonStore
    {
        private readonly List<AllocatableSalesPerson> _salesPerson;

        public SalesPersonStore()
        {
            _salesPerson = new List<AllocatableSalesPerson>();
        }

        /// <exception cref="SalesPersonAlreadyExistsException">Thrown if the SalesPerson requested to be allocated already exists.</exception>
        public void RegisterNewSalesPerson(
            AllocatableSalesPerson salesPerson)
            => _salesPerson.Add(salesPerson);

        public IEnumerable<AllocatableSalesPerson> FindMatchingSalesPerson(
            Predicate<AllocatableSalesPerson> matcher)
            => _salesPerson.Where(matcher.Invoke);
    }

    public class SalesPersonAlreadyExistsException : Exception
    {
        public SalesPersonAlreadyExistsException(string errorMsg = "")
            : base(errorMsg) { }
    }
}
