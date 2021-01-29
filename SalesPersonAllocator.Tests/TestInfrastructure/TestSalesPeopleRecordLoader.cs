using System.Collections.Generic;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.Tests.TestInfrastructure
{
    public class TestSalesPeopleRecordLoader : ISalesPeopleRecordLoader
    {
        public IEnumerable<AllocatableSalesPerson> GetSalesPeople()
        {
            return new List<AllocatableSalesPerson>();
        }
    }
}
