using System.Collections.Generic;
using System.Linq;
using DomainLogic.DomainModels;

namespace DomainLogic.DomainLogic
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
