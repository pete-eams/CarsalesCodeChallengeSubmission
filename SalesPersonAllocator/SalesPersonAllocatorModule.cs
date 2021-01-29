﻿using Autofac;
using SalesPersonAllocator.DomainLogic;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.Infrastructure;

namespace SalesPersonAllocator
{
    public class SalesPersonAllocatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SalesPersonStore>().SingleInstance();
            builder.RegisterType<SalesPersonMapFactory>().SingleInstance();
            builder.RegisterType<SalesPersonAllocationProvider>().SingleInstance();
            builder.RegisterType<Dispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SalesPeopleRecordLoader>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
