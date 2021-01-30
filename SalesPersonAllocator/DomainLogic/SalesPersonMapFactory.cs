using System;
using System.Collections.Generic;
using System.Linq;
using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocator.DomainModels.Enums;

namespace SalesPersonAllocator.DomainLogic
{
    public class SalesPersonMapFactory
    {
        private readonly Func<SalesPersonCriteria, IHandler> _allocationRuleHandlerFactory;

        public SalesPersonMapFactory(
            Func<SalesPersonCriteria, AllocationRuleHandler> allocationRuleHandlerFactory)
        {
            _allocationRuleHandlerFactory = allocationRuleHandlerFactory;
        }

        public List<SalesPersonAssignmentHandler> Create()
            => new List<SalesPersonAssignmentHandler>
            {
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(SpeaksGreek, WantSportsCar), CreateHandlers
                        .WithSpecialty(SalesGroup.SpeaksGreek, SalesGroup.SportsCarSpecialist)
                        .WithSpecialty(SalesGroup.SportsCarSpecialist)
                        .WithSpecialty(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(SpeaksGreek, WantFamilyCar), CreateHandlers
                        .WithSpecialty(SalesGroup.SpeaksGreek, SalesGroup.FamilyCarSpecialist)
                        .WithSpecialty(SalesGroup.FamilyCarSpecialist)
                        .WithSpecialty(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(RegardlessOfLanguage, WantTradieCar), CreateHandlers
                        .WithSpecialty(SalesGroup.TradeVehicleSpecialist)
                        .WithSpecialty(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, WantSportsCar), CreateHandlers
                        .WithSpecialty(SalesGroup.SportsCarSpecialist)
                        .WithSpecialty(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, WantFamilyCar), CreateHandlers
                        .WithSpecialty(SalesGroup.FamilyCarSpecialist)
                        .WithSpecialty(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(SpeaksGreek, NotLookForAnythingSpecific), CreateHandlers
                        .WithSpecialty(SalesGroup.SpeaksGreek)
                        .WithSpecialty(AnyOne)
                        .Build()),
                
                new SalesPersonAssignmentHandler(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, NotLookForAnythingSpecific), CreateHandlers
                        .WithSpecialty(AnyOne)
                        .Build()),
            };

        private static SalesGroup[] AnyOne 
            => new SalesGroup[] {};
        
        private AllocationRuleHandlerBuilder CreateHandlers
            => new AllocationRuleHandlerBuilder(_allocationRuleHandlerFactory);
        
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
            private readonly List<IHandler> _handlers;
            private readonly Func<SalesPersonCriteria, IHandler> _allocationRuleHandlerFactory;

            public AllocationRuleHandlerBuilder(
                Func<SalesPersonCriteria, IHandler> allocationRuleHandlerFactory)
            {
                _allocationRuleHandlerFactory = allocationRuleHandlerFactory;
                _handlers = new List<IHandler>();
            }
            
            public AllocationRuleHandlerBuilder WithSpecialty(
                params SalesGroup[] salesGroups)
            {
                _handlers.Add(_allocationRuleHandlerFactory
                    .Invoke(SalesPersonCriteria.WithCriteria(salesGroups)));
                
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
