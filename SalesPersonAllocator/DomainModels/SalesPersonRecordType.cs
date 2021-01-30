﻿using System.Collections.Generic;

namespace SalesPersonAllocator.DomainModels
{
    public class SalesPersonRecordType
    {
        public IEnumerable<SalesPersonType> SalesPersonTypes { get; set; }
    }

    public class SalesPersonType
    {
        public string Name { get; set; }

        public IEnumerable<string> Groups { get; set; }
    }
}
