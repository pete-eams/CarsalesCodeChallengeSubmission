using DomainLogic.DomainModels.Enums;
using System;
using System.Collections.Generic;

namespace DomainLogic.DomainModels
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
            return obj.GetType() == this.GetType() && Equals((SalesPerson) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_groups != null ? _groups.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}
