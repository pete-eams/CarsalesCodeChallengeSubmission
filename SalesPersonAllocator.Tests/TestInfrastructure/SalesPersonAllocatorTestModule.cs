using Autofac;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.Infrastructure;

namespace SalesPersonAllocator.Tests.TestInfrastructure
{
    class SalesPersonAllocatorTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new BehaviourConfiguration());
            builder.RegisterType<AllocationRuleHandler>().AsSelf();
            builder.RegisterType<SalesPersonStore>().SingleInstance();
            builder.RegisterType<SalesPersonMapFactory>().SingleInstance();
            builder.RegisterType<SalesPersonAllocationProvider>().SingleInstance();
            builder.RegisterType<Dispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<TestSalesPeopleRecordLoader>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
