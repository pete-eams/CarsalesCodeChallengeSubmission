using DomainLogic.DomainModels.Enums;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocator.Tests
{
    public class DoesNotSpeakGreekFamilyCarSeekingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsFamilyCarWillBeAllocatedAGreekSportsCarSpecialistWhenOneIsAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsFamilyCarWillBeAllocatedAGreekSportsCarSpecialistWhenOneIsAvailableCaseTwo()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndFamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsFamilyCarWillBeAllocatedAnyoneIfTheFirstOptionIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsFamilyCarWillBeAllocatedAnyoneIfTheFirstOptionIsAllocated()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndFamilyCarSpecialist))
                .And(s => ASalePersonHasBeenAllocated(GreekAndFamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void NoOneWillBeAllocatedIfNoSalesPersonsAreAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => AllSalesPersonAreAllocated())
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Family)))
                .Then(s => s.CustomerIsAllocatedTo(InvalidSalesPerson))
                .BDDfy();
    }
}
