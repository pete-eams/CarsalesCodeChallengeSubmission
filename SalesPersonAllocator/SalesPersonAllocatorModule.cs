using Autofac;
using Autofac.Builder;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.Infrastructure;

namespace SalesPersonAllocator
{
    public class SalesPersonAllocatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Dispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPersonStore>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPersonMapFactory>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPersonAllocationProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AllocationRuleHandler>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
