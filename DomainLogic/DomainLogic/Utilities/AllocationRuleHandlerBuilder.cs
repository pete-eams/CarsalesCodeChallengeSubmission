using System;
using DomainLogic.DomainLogic.Interfaces;
using DomainLogic.DomainModels;
using DomainLogic.DomainModels.Enums;

namespace DomainLogic.DomainLogic.Utilities
{
    public class AllocationRuleHandlerBuilder : HandlerBuilder
    {
        private readonly Func<SalesPersonCriteria, IHandler> _allocationRuleHandlerFactory;

        public AllocationRuleHandlerBuilder(
            Func<SalesPersonCriteria, IHandler> allocationRuleHandlerFactory)
        {
            _allocationRuleHandlerFactory = allocationRuleHandlerFactory;
        }

        public AllocationRuleHandlerBuilder HandleWith(
            params SalesGroup[] salesGroups)
        {
            AddHandler(_allocationRuleHandlerFactory
                .Invoke(SalesPersonCriteria.WithCriteria(salesGroups)));

            return this;
        }
    }
}
