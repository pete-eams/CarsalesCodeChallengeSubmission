using System.Collections.Generic;

namespace SalesPersonAllocator.DomainModels
{
    public class SalesPerson
    {
        private readonly HashSet<SalesGroup> _groups;
        
        public SalesPerson(string name, params SalesGroup[] groups)
        {
            Name = name;
            _groups = new HashSet<SalesGroup>(groups);
        }

        public string Name { get; }
        
        public bool BelongsToGroup(SalesGroup group) 
            => _groups.Contains(group);
    }

    public enum SalesGroup
    {
        SpeaksGreek,
        SportsCarSpecialist,
        FamilyCarSpecialist,
        TradeVehicleSpecialist
    }
}
