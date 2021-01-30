using System.Collections.Generic;
using System.IO;
using Autofac;
using Newtonsoft.Json;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.Infrastructure;

namespace SalesPersonAllocator
{
    public class SalesPersonAllocatorModule : Module
    {
        private readonly BehaviourConfiguration _behaviourConfig;
        private readonly SalesPersonRecordType _salesPersonRecord;

        public SalesPersonAllocatorModule()
        {
            _behaviourConfig = LoadBehaviourConfig();
            _salesPersonRecord = LoadSalesPerson();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_behaviourConfig);
            builder.RegisterInstance(_salesPersonRecord);
            builder.RegisterType<AllocationRuleHandler>().AsSelf();
            builder.RegisterType<SalesPersonStore>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPersonMapFactory>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPersonAllocationProvider>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<Dispatcher>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPeopleRecordLoader>().AsSelf().AsImplementedInterfaces().SingleInstance();
        }

        private BehaviourConfiguration LoadBehaviourConfig()
            => LoadExternalConfig<BehaviourConfiguration>("BehaviourConfiguration.json");

        private SalesPersonRecordType LoadSalesPerson()
            => new SalesPersonRecordType { SalesPersonTypes = LoadExternalConfig<IEnumerable<SalesPersonType>>("SalesPerson.json") };

        private T LoadExternalConfig<T>(string path) 
            => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}
