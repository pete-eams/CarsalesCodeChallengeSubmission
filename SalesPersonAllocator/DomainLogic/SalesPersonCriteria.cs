using System.Collections.Generic;
using System.Linq;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    class SalesPersonCriteria
    {
        private readonly List<SalesGroup> _salesGroups;
        
        public SalesPersonCriteria(params SalesGroup[] groups)
        {
            _salesGroups = groups.ToList();
        }

        public bool Matches(SalesPerson salesPerson)
            => BelongToRequiredGroups(salesPerson) && !salesPerson.IsAllocated;

        private bool BelongToRequiredGroups(SalesPerson salesPerson)
            => _salesGroups.All(salesPerson.BelongsToGroup);
    }
}
