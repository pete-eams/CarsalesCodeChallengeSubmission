using System.Collections.Generic;
using SalesPersonAllocator.DomainLogic;

namespace SalesPersonAllocator.DomainModels
{
    public class AllocatableSalesPerson : SalesPerson
    {
        public AllocatableSalesPerson(
            string name, params SalesGroup[] groups) 
            : base(name, groups)
        {
        }
        
        public bool IsAllocated { get; private set; }

        public void AllocateToCustomer()
            => IsAllocated = true;

        public static AllocatableSalesPerson FromRecord(SalesPersonType type)
        {
            var groups = new List<SalesGroup>();
            foreach (var group in type.Groups)
            {
                switch (@group)
                {
                    case "A":
                        groups.Add(SalesGroup.SpeaksGreek);
                        break;
                    case "B":
                        groups.Add(SalesGroup.SportsCarSpecialist);
                        break;
                    case "C":
                        groups.Add(SalesGroup.FamilyCarSpecialist);
                        break;
                    case "D":
                        groups.Add(SalesGroup.TradeVehicleSpecialist);
                        break;
                }
            }

            return new AllocatableSalesPerson(type.Name, groups.ToArray());
        }
    }
}