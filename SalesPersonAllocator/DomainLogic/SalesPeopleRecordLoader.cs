using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPeopleRecordLoader : ISalesPeopleRecordLoader
    {
        private readonly SalesPersonRecordType _salesPersonRecordType;
        
        public SalesPeopleRecordLoader(SalesPersonRecordType salesPersonRecordType)
        {
            _salesPersonRecordType = salesPersonRecordType;
        }
        
        public IEnumerable<AllocatableSalesPerson> GetSalesPeople()
            => _salesPersonRecordType.SalesPersonTypes
                .Select(AllocatableSalesPerson.FromRecord);
    }

    public interface ISalesPeopleRecordLoader 
    {
        IEnumerable<AllocatableSalesPerson> GetSalesPeople();
    }
}
