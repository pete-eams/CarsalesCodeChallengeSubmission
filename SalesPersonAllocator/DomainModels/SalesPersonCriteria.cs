using System.Collections.Generic;
using System.Linq;

namespace SalesPersonAllocator.DomainModels
{
    class SalesPersonCriteria
    {
        private readonly List<SalesGroup> _salesGroups;
        
        private SalesPersonCriteria(params SalesGroup[] groups)
        {
            _salesGroups = groups.ToList();
        }

        public static SalesPersonCriteria WithCriteria(params SalesGroup[] groups)
            => new SalesPersonCriteria(groups);

        public bool Matches(SalesPerson salesPerson)
            => BelongToRequiredGroups(salesPerson) && !salesPerson.IsAllocated;

        private bool BelongToRequiredGroups(SalesPerson salesPerson)
            => _salesGroups.All(salesPerson.BelongsToGroup);
    }
}
