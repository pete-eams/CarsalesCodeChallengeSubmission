using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.Infrastructure;
using SalesPersonAllocator.Infrastructure.Interfaces;
using SalesPersonAllocator.ViewModels;

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
        public async Task<IActionResult> AllocateSalesPerson(
            [FromBody] CustomerPreferenceViewModel customerPreference)
        {
            return await _taskReceiver.AddHttpTask(
                () => Ok(_salesPersonAllocator
                    .GetAllocation(customerPreference.ToDomainEntity())));
        }
    }
}
