using System;
using Autofac;
using SalesPersonAllocator;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.Infrastructure.Interfaces;

namespace SalesPersonAllocatorTest.TestInfrastructure
{
    class TestModule
    {
        public TestModule()
        {
        }

        public ITaskReceiver TaskReceiver { get; private set; }
        public SalesPersonStore SalesPersonStore { get; private set; }
        public SalesPersonAllocationProvider Allocator { get; private set; }

        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<SalesPersonAllocatorModule>();

            var container = builder.Build();
            TaskReceiver = container.Resolve<ITaskReceiver>();
            Allocator = container.Resolve<SalesPersonAllocationProvider>();
            SalesPersonStore = container.Resolve<SalesPersonStore>();
        }
    }
}
