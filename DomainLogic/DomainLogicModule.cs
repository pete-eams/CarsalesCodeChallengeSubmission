using Autofac;
using DomainLogic.DomainLogic;
using DomainLogic.DomainModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DomainLogic
{
    public class DomainLogicModule : Module
    {
        private readonly BehaviourConfiguration _behaviourConfig;
        private readonly SalesPersonRecordType _salesPersonRecord;

        public DomainLogicModule()
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
            builder.RegisterType<SalesPeopleRecordLoader>().AsSelf().AsImplementedInterfaces().SingleInstance();
        }

        private static BehaviourConfiguration LoadBehaviourConfig()
            => LoadExternalConfig<BehaviourConfiguration>("BehaviourConfiguration.json");

        private static SalesPersonRecordType LoadSalesPerson()
            => new SalesPersonRecordType { SalesPersonTypes = LoadExternalConfig<IEnumerable<SalesPersonType>>("SalesPerson.json") };

        private static T LoadExternalConfig<T>(string path) 
            => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}
