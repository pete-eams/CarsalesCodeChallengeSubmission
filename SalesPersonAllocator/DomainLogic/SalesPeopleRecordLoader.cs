using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPeopleRecordLoader : ISalesPeopleRecordLoader
    {
        public IEnumerable<AllocatableSalesPerson> GetSalesPeople()
        {
            var record = JsonConvert.DeserializeObject<IEnumerable<SalesPersonType>>(File.ReadAllText("SalesPerson.json"));
            foreach (var salesPerson in record)
            {
                yield return AllocatableSalesPerson.FromRecord(salesPerson);
            }
        }
    }

    public interface ISalesPeopleRecordLoader
    {
        IEnumerable<AllocatableSalesPerson> GetSalesPeople();
    }
}
