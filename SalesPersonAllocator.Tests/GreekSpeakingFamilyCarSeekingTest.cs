using DomainLogic.DomainModels.Enums;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocator.Tests
{
    public class GreekSpeakingFamilyCarSeekingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsFamilyCarWillBeAllocatedAGreekFamilyCarSpecialistWhenOneIsAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndFamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsFamilyCarWillBeAllocatedAFamilyCarSpecialistIfTheGreekSpeakingOneIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsFamilyCarWillBeAllocatedAnyoneIfTheFirstTwoOptionsAreNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekAndWantsFamilyCarWillBeAllocatedASportsCarSpecialistIfTheFirstOptionIsAllocated()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .And(s => ASalePersonHasBeenAllocated(GreekAndFamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void NoOneWillBeAllocatedIfNoSalesPersonsAreAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => AllSalesPersonAreAllocated())
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(InvalidSalesPerson))
                .BDDfy();
    }
}
