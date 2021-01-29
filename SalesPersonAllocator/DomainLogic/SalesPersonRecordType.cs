using System.Collections.Generic;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPersonType
    {
        public string Name { get; set; }

        public IEnumerable<string> Groups { get; set; }
    }
}
