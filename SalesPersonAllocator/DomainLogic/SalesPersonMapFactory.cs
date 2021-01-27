using System.Collections.Generic;
using System.Linq;
using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    class SalesPersonMapFactory
    {
        private readonly SalesPersonStore _store;

        public SalesPersonMapFactory(SalesPersonStore store)
        {
            _store = store;
        }
    
        public Dictionary<CustomerPreference, IHandler> Create()
            => new Dictionary<CustomerPreference, IHandler>
            {
                {
                    CustomerPreference(LanguagePreference.Greek, CarPreference.Sports), 
                    CreateRule
                        .WithOrderedCriteria(SalesGroup.SpeaksGreek, SalesGroup.SportsCarSpecialist)
                        .WithOrderedCriteria(SalesGroup.SportsCarSpecialist)
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                },
                {
                    CustomerPreference(LanguagePreference.Greek, CarPreference.Family), 
                    CreateRule
                        .WithOrderedCriteria(SalesGroup.SpeaksGreek, SalesGroup.FamilyCarSpecialist)
                        .WithOrderedCriteria(SalesGroup.FamilyCarSpecialist)
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                },
                {
                    CustomerPreference(LanguagePreference.NoPreference, CarPreference.Tradie), 
                    CreateRule
                        .WithOrderedCriteria(SalesGroup.TradeVehicleSpecialist)
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                },
                {
                    CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Sports), 
                    CreateRule
                        .WithOrderedCriteria(SalesGroup.SportsCarSpecialist)
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                },
                {
                    CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Family), 
                    CreateRule
                        .WithOrderedCriteria(SalesGroup.FamilyCarSpecialist)
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                },
                {
                    CustomerPreference(LanguagePreference.Greek, CarPreference.NoPreference), 
                    CreateRule
                        .WithOrderedCriteria(SalesGroup.SpeaksGreek)
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                },
                {
                    CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.NoPreference), 
                    CreateRule
                        .WithOrderedCriteria(AnyOne)
                        .Build()
                }
            };

        private static SalesGroup[] AnyOne 
            => new SalesGroup[] {};
        
        
        private AllocationRuleHandlerBuilder CreateRule
            => new AllocationRuleHandlerBuilder(_store);

        private static CustomerPreference CustomerPreference(
            LanguagePreference langPref,
            CarPreference carPref)
            => new CustomerPreference(langPref, carPref);

        private class AllocationRuleHandlerBuilder
        {
            private readonly SalesPersonStore _store;
            private readonly List<AllocationRuleHandler> _handlers;
            
            public AllocationRuleHandlerBuilder(SalesPersonStore store)
            {
                _store = store;
                _handlers = new List<AllocationRuleHandler>();
            }
            
            public AllocationRuleHandlerBuilder WithOrderedCriteria(params SalesGroup[] salesGroups)
            {
                _handlers.Add(
                    new AllocationRuleHandler(_store, 
                        SalesPersonCriteria.WithCriteria(salesGroups)));
                return this;
            }

            public AllocationRuleHandler Build()
            {
                for (var i = 0; i < _handlers.Count - 1; i++)
                {
                    var currentHandler = _handlers[i];
                    var nextHandler = _handlers[i+1];

                    currentHandler.SetNext(nextHandler);
                }

                return _handlers.First();
            }
        }
    }
}
