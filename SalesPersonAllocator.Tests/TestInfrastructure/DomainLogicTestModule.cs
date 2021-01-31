using Autofac;
using DomainLogic;
using DomainLogic.DomainLogic;
using DomainLogic.DomainModels;

namespace SalesPersonAllocator.Tests.TestInfrastructure
{
    internal class DomainLogicTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new BehaviourConfiguration());
            builder.RegisterType<AllocationRuleHandler>().AsSelf();
            builder.RegisterType<SalesPersonStore>().SingleInstance();
            builder.RegisterType<SalesPersonMapFactory>().SingleInstance();
            builder.RegisterType<SalesPersonAllocationProvider>().SingleInstance();
            builder.RegisterType<TestSalesPeopleRecordLoader>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
