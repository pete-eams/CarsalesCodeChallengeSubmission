using SalesPersonAllocator.DomainModels;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocatorTest
{
    public class DoesNotSpeakGreekAndSportsCarSeekingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsSportsCarWillBeAllocatedAGreekSportsCarSpecialistWhenOneIsAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();
        
        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsSportsCarWillBeAllocatedAGreekSportsCarSpecialistWhenOneIsAvailableCaseTwo()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(GreekAndSportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsSportsCarWillBeAllocatedAnyoneIfTheFirstOptionIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoDoesNotSpeaksGreekAndWantsSportsCarWillBeAllocatedAnyoneIfTheFirstOptionIsAllocated()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => ASalePersonHasBeenAllocated(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(FamilyCarSpecialist))
                .BDDfy();
        
        [Fact]
        public void NoOneWillBeAllocatedIfNoSalesPersonsAreAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(GreekAndSportsCarSpecialist))
                .And(s => SalesPersonIsAvailable(FamilyCarSpecialist))
                .And(s => AllSalesPersonAreAllocated())
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.DoesNotSpeakGreek, CarPreference.Sports)))
                .Then(s => s.CustomerIsAllocatedTo(InvalidSalesPerson))
                .BDDfy();
    }
}
