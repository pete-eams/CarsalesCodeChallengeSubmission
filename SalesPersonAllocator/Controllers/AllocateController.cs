﻿using Microsoft.AspNetCore.Mvc;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DTOs;

namespace SalesPersonAllocator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllocateController : ControllerBase
    {    
        private readonly SalesPersonAllocationProvider _salesPersonAllocator;

        public AllocateController(
            SalesPersonAllocationProvider salesPersonAllocator)
        {
            _salesPersonAllocator = salesPersonAllocator;
        }
        
        [HttpPost]
        public SalesPersonDto AllocateSalesPerson(
            [FromBody] CustomerPreferenceDto customerPreference)
        {
            var allocation = _salesPersonAllocator
                        .GetAllocation(customerPreference.ToDomainEntity());
            
            return SalesPersonDto.FromDomainEntity(allocation);
        }
    }
}