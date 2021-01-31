using DomainLogic.DomainLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLogic.DomainModels
{
    public class SalesPersonStore
    {
        private readonly List<AllocatableSalesPerson> _salesPerson;

        public SalesPersonStore(ISalesPeopleRecordLoader recordLoader)
        {
            _salesPerson = new List<AllocatableSalesPerson>();
            
            foreach (var salesPerson in recordLoader.GetSalesPeople())
                RegisterNewSalesPerson(salesPerson);
        }
        public void RegisterNewSalesPerson(
            AllocatableSalesPerson salesPerson)
            => _salesPerson.Add(salesPerson);

        public IEnumerable<AllocatableSalesPerson> FindMatchingSalesPerson(
            Predicate<AllocatableSalesPerson> matcher)
            => _salesPerson.Where(matcher.Invoke);
    }
}
