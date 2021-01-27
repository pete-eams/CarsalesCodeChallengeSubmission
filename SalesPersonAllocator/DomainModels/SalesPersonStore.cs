using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesPersonAllocator.DomainModels
{
    public class SalesPersonStore
    {
        private readonly List<SalesPerson> _salesPerson;

        public SalesPersonStore()
        {
            _salesPerson = new List<SalesPerson>();
        }

        /// <exception cref="SalesPersonAlreadyExistsException">Thrown if the SalesPerson requested to be allocated already exists.</exception>
        public void RegisterNewSalesPerson(
            SalesPerson salesPerson)
            => _salesPerson.Add(salesPerson);

        public IEnumerable<SalesPerson> FindMatchingSalesPerson(
            Predicate<SalesPerson> matcher)
            => _salesPerson.Where(matcher.Invoke);

        // TODO: Does this belong outside this class?
        /// <exception cref="SalesPersonDoesNotExistException">Thrown if the SalesPerson requested to be allocated does not exist.</exception>
        /// <exception cref="SalesPersonAlreadyAllocatedException">Thrown if the SalesPerson requested to be allocated has already been allocated.</exception>
        public void AllocateSalesPerson(SalesPerson salesPerson)
        {
            var salesPersonToAllocate = _salesPerson
                .FirstOrDefault(s => s.Equals(salesPerson));

            if (salesPersonToAllocate == null)
                throw new SalesPersonDoesNotExistException();

            if (salesPersonToAllocate.IsAllocated)
                throw new SalesPersonAlreadyAllocatedException();

            salesPersonToAllocate.AllocateToCustomer();
        }
    }

    public class SalesPersonAlreadyExistsException : Exception
    {
        public SalesPersonAlreadyExistsException(string errorMsg = "")
            : base(errorMsg) { }
    }

    public class SalesPersonDoesNotExistException : Exception
    {
        public SalesPersonDoesNotExistException(string errorMsg = "") 
            : base(errorMsg) { }
    }

    public class SalesPersonAlreadyAllocatedException : Exception
    {
        public SalesPersonAlreadyAllocatedException(string errorMsg = "")
            : base(errorMsg) { }
    }
}
