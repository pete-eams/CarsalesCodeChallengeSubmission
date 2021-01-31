using DomainLogic.DomainModels.Enums;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocator.Tests
{
    public class GreekSpeakingSportsCarSeekingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsSportsCarWillBeAllocatedAGreekSportsCarSpecialistWhenOneIsAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndSportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsSportsCarWillBeAllocatedASportsCarSpecialistIfTheGreekSpeakingOneIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsSportsCarWillBeAllocatedASportsCarSpecialistIfTheFirstOptionIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsSportsCarWillBeAllocatedASportsCarSpecialistIfTheFirstOptionIsAllocated()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .And(s => ASalePersonHasBeenAllocated(GreekAndSportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void NoOneWillBeAllocatedIfNoSalesPersonsAreAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => AllSalesPersonAreAllocated())
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(InvalidSalesPerson))
                .BDDfy();
    }
}
