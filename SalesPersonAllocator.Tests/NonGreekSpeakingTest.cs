using DomainLogic.DomainModels.Enums;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocator.Tests
{
    public class NonGreekSpeakingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoDoesNotSpeakGreekAndHasNoCarPreferenceWillBeAllocatedAnyone()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndFamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoDoesNotSpeakGreekAndHasNoCarPreferenceWillBeAllocatedAnyoneCaseTwo()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .And(s => ASalePersonHasBeenAllocated(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void NoOneWillBeAllocatedIfNoSalesPersonsAreAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => AllSalesPersonAreAllocated())
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.NoPreference)))
                .Then(s => s.CustomerIsAllocatedTo(InvalidSalesPerson))
                .BDDfy();
    }
}
