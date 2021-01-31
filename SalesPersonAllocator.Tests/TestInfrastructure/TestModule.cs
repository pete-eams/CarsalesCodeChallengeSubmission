using Autofac;
using DomainLogic.DomainLogic;
using DomainLogic.DomainModels;

namespace SalesPersonAllocator.Tests.TestInfrastructure
{
    internal class TestModule
    {
        public SalesPersonStore SalesPersonStore { get; private set; }
        public SalesPersonAllocationProvider Allocator { get; private set; }

        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DomainLogicTestModule>();

            var container = builder.Build();
            Allocator = container.Resolve<SalesPersonAllocationProvider>();
            SalesPersonStore = container.Resolve<SalesPersonStore>();
        }
    }
}
