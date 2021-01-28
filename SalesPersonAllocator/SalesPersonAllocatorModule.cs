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
            builder.RegisterType<SalesPersonStore>().SingleInstance();
            builder.RegisterType<SalesPersonMapFactory>().SingleInstance();
            builder.RegisterType<SalesPersonAllocationProvider>().SingleInstance();
        }
    }
}
