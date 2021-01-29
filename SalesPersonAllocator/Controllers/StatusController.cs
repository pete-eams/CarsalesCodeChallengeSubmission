using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.DTOs;

namespace SalesPersonAllocator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly SalesPersonStore _store;

        public StatusController(
            SalesPersonStore store)
        {
            _store = store;
        }

        [HttpGet]
        public IEnumerable<SalesPersonDto> AllocateSalesPerson()
            => _store.FindMatchingSalesPerson(Any)
                .Select(SalesPersonDto.FromDomainEntity)
                .ToList();

        private static bool Any(AllocatableSalesPerson s) 
            => true;
    }
}
