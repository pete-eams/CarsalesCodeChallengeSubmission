using DomainLogic.DomainLogic;
using DomainLogic.DomainModels;
using System.Collections.Generic;

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
