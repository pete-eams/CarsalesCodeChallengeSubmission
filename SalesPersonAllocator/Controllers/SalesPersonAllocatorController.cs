using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DTOs;
using SalesPersonAllocator.Infrastructure;
using SalesPersonAllocator.Infrastructure.Interfaces;

namespace SalesPersonAllocator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesPersonAllocatorController : ControllerBase
    {    
        private readonly ITaskReceiver _taskReceiver;
        private readonly SalesPersonAllocationProvider _salesPersonAllocator;

        public SalesPersonAllocatorController(
            ITaskReceiver taskReceiver,
            SalesPersonAllocationProvider salesPersonAllocator)
        {
            _taskReceiver = taskReceiver;
            _salesPersonAllocator = salesPersonAllocator;
        }

        [HttpPost]
        [Route("allocate")]
        public async Task<IActionResult> AllocateSalesPerson(
            [FromBody] CustomerPreferenceDto customerPreference)
        {
            return await _taskReceiver.AddHttpTask(
                () =>
                {
                    var allocation = _salesPersonAllocator
                        .GetAllocation(customerPreference.ToDomainEntity());
                    
                    return Ok(SalesPersonDto.FromDomainEntity(allocation));
                });
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        [Route("test")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
