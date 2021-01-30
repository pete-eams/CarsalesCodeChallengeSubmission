using System;
using System.Collections.Generic;
using System.Linq;
using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPersonMapFactory
    {
        private readonly SalesPersonStore _store;

        public SalesPersonMapFactory(SalesPersonStore store)
        {
            _store = store;
        }

        public List<SalesPersonAssignmentHandler> Create()
            => new List<SalesPersonAssignmentHandler>
            {
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(SpeaksGreek, WantSportsCar), CreateRule
                        .WithAssignPriority(SalesGroup.SpeaksGreek, SalesGroup.SportsCarSpecialist)
                        .WithAssignPriority(SalesGroup.SportsCarSpecialist)
                        .WithAssignPriority(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(SpeaksGreek, WantFamilyCar), CreateRule
                        .WithAssignPriority(SalesGroup.SpeaksGreek, SalesGroup.FamilyCarSpecialist)
                        .WithAssignPriority(SalesGroup.FamilyCarSpecialist)
                        .WithAssignPriority(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(RegardlessOfLanguage, WantTradieCar), CreateRule
                        .WithAssignPriority(SalesGroup.TradeVehicleSpecialist)
                        .WithAssignPriority(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, WantSportsCar), CreateRule
                        .WithAssignPriority(SalesGroup.SportsCarSpecialist)
                        .WithAssignPriority(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, WantFamilyCar), CreateRule
                        .WithAssignPriority(SalesGroup.FamilyCarSpecialist)
                        .WithAssignPriority(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(SpeaksGreek, NotLookForAnythingSpecific), CreateRule
                        .WithAssignPriority(SalesGroup.SpeaksGreek)
                        .WithAssignPriority(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, NotLookForAnythingSpecific), CreateRule
                        .WithAssignPriority(AnyOne)
                        .Build()),
            };

        private static SalesGroup[] AnyOne 
            => new SalesGroup[] {};
        
        private AllocationRuleHandlerBuilder CreateRule
            => new AllocationRuleHandlerBuilder(_store);
        

        private static CustomerPreferenceCondition CustomerPreferenceCondition(
            Predicate<LanguagePreference> langPredicate,
            Predicate<CarPreference> carPredicate)
            => new CustomerPreferenceCondition(carPredicate, langPredicate);

        private static bool SpeaksGreek(LanguagePreference lang) 
            => lang == LanguagePreference.Greek;

        private static bool DoesNotSpeaksGreek(LanguagePreference lang)
            => lang == LanguagePreference.DoesNotSpeakGreek;

        private static bool RegardlessOfLanguage(LanguagePreference lang)
            => true;

        private static bool WantSportsCar(CarPreference car)
            => car == CarPreference.Sports;

        private static bool WantFamilyCar(CarPreference car)
            => car == CarPreference.Family;

        private static bool WantTradieCar(CarPreference car)
            => car == CarPreference.Tradie;

        private static bool NotLookForAnythingSpecific(CarPreference car)
            => true;

        private class AllocationRuleHandlerBuilder
        {
            private readonly SalesPersonStore _store;
            private readonly List<IHandler> _handlers;
            
            public AllocationRuleHandlerBuilder(SalesPersonStore store)
            {
                _store = store;
                _handlers = new List<IHandler>();
            }
            
            public AllocationRuleHandlerBuilder WithAssignPriority(params SalesGroup[] salesGroups)
            {
                _handlers.Add(
                    new AllocationRuleHandler(_store, 
                        SalesPersonCriteria.WithCriteria(salesGroups)));
                return this;
            }

            public IHandler Build()
            {
                // linking the handlers according to the "chain of responsibility"
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
