using SalesPersonAllocator.DomainModels;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocatorTest
{
    public class GreekSpeakingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoSpeaksGreekWillBeAllocatedAGreekSpeakingSpecialistWhenOneIsAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndFamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekWillBeAllocatedAGreekSpeakingSpecialistWhenOneIsAvailableCaseTwo()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndFamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekWillBeAllocatedAnyoneIfTheFirstOptionIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoSpeaksGreekWillBeAllocatedAnyoneIfTheFirstOptionIsAllocated()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .And(s => ASalePersonHasBeenAllocated(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void NoOneWillBeAllocatedIfNoSalesPersonsAreAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => AllSalesPersonAreAllocated())
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.Greek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(InvalidSalesPerson))
                .BDDfy();
    }
}
