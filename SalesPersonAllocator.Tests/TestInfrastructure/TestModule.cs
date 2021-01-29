using Autofac;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.Infrastructure.Interfaces;

namespace SalesPersonAllocator.Tests.TestInfrastructure
{
    internal class TestModule
    {
        public ITaskReceiver TaskReceiver { get; private set; }
        public SalesPersonStore SalesPersonStore { get; private set; }
        public SalesPersonAllocationProvider Allocator { get; private set; }

        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<SalesPersonAllocatorTestModule>();

            var container = builder.Build();
            TaskReceiver = container.Resolve<ITaskReceiver>();
            Allocator = container.Resolve<SalesPersonAllocationProvider>();
            SalesPersonStore = container.Resolve<SalesPersonStore>();
        }
    }
}
