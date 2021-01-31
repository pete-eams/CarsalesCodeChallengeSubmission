using DomainLogic.DomainLogic.Interfaces;
using DomainLogic.DomainLogic.Utilities;
using DomainLogic.DomainModels;
using DomainLogic.DomainModels.Enums;
using System;

namespace DomainLogic.DomainLogic
{
    public class SalesPersonMapFactory
    {
        private readonly Func<SalesPersonCriteria, IHandler> _allocationRuleHandlerFactory;

        public SalesPersonMapFactory(
            Func<SalesPersonCriteria, AllocationRuleHandler> allocationRuleHandlerFactory)
        {
            _allocationRuleHandlerFactory = allocationRuleHandlerFactory;
        }

        public IHandler Create()
        {
            return new CustomerPreferenceHandlerBuilder()
                .WithCustomerPreference(
                    CustomerPreferenceCondition(SpeaksGreek, WantSportsCar), CreateHandlers
                        .HandleWith(SalesGroup.SpeaksGreek, SalesGroup.SportsCarSpecialist)
                        .HandleWith(SalesGroup.SportsCarSpecialist)
                        .HandleWith(AnyOne)
                        .Build())
                
                .WithCustomerPreference(
                    CustomerPreferenceCondition(SpeaksGreek, WantFamilyCar), CreateHandlers
                        .HandleWith(SalesGroup.SpeaksGreek, SalesGroup.FamilyCarSpecialist)
                        .HandleWith(SalesGroup.FamilyCarSpecialist)
                        .HandleWith(AnyOne)
                        .Build())
                
                .WithCustomerPreference(
                    CustomerPreferenceCondition(RegardlessOfLanguage, WantTradieCar), CreateHandlers
                        .HandleWith(SalesGroup.TradeVehicleSpecialist)
                        .HandleWith(AnyOne)
                        .Build())
                
                .WithCustomerPreference(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, WantSportsCar), CreateHandlers
                        .HandleWith(SalesGroup.SportsCarSpecialist)
                        .HandleWith(AnyOne)
                        .Build())
                
                .WithCustomerPreference(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, WantFamilyCar), CreateHandlers
                        .HandleWith(SalesGroup.FamilyCarSpecialist)
                        .HandleWith(AnyOne)
                        .Build())
                
                .WithCustomerPreference(
                    CustomerPreferenceCondition(SpeaksGreek, NotLookForAnythingSpecific), CreateHandlers
                        .HandleWith(SalesGroup.SpeaksGreek)
                        .HandleWith(AnyOne)
                        .Build())
                
                .WithCustomerPreference(
                    CustomerPreferenceCondition(DoesNotSpeaksGreek, NotLookForAnythingSpecific), CreateHandlers
                        .HandleWith(AnyOne)
                        .Build())
                
                .Build();
        }
        
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
    }
}
