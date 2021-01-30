using System;
using System.Collections.Generic;
using SalesPersonAllocator.DomainModels.Enums;

namespace SalesPersonAllocator.DomainModels
{
    public class SalesPerson : IEquatable<SalesPerson>
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

        public bool Equals(SalesPerson other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && _groups.SetEquals(other._groups);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SalesPerson) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_groups, Name);
        }
    }
}
