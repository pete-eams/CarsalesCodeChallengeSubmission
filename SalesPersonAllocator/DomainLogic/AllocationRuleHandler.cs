﻿using System.Linq;
using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    class AllocationRuleHandler : IHandler
    {
        private IHandler _nextHandler;
        private readonly SalesPersonStore _store;
        private readonly SalesPersonCriteria _salesPersonCriteria;
        
        public AllocationRuleHandler(
            SalesPersonStore store,
            SalesPersonCriteria salesPersonCriteria)
        {
            _store = store;
            _salesPersonCriteria = salesPersonCriteria;
        }

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public object Handle()
        {
            var matchingSalesPersons = _store
                .FindMatchingSalesPerson(SalesPersonMatchingCriteria);
            
            var matchingSalesPerson = matchingSalesPersons.FirstOrDefault();
            if (matchingSalesPerson == null)
                return _nextHandler?.Handle();

            matchingSalesPerson.AllocateToCustomer();
            return matchingSalesPerson;
        }

        private bool SalesPersonMatchingCriteria(AllocatableSalesPerson salesPerson) 
            => _salesPersonCriteria.Matches(salesPerson);
    }
}
