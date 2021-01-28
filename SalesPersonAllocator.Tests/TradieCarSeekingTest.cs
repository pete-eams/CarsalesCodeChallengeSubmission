using SalesPersonAllocator.DomainModels;
using TestStack.BDDfy;
using Xunit;

namespace SalesPersonAllocator.Tests
{
    public class TradieCarSeekingTest : AllocationTestBase
    {
        [Fact]
        public void CustomerWhoWantsTradieCarWillBeAllocatedATradieCarSpecialistWhenOneIsAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(TradeVehicleSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.NoPreference, CarPreference.Tradie)))
                .Then(s => s.CustomerIsAllocatedTo(TradeVehicleSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoWantsTradieCarWillBeAllocatedAnyoneIfTheFirstOptionIsNotAvailable()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.NoPreference, CarPreference.Tradie)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();

        [Fact]
        public void CustomerWhoWantsTradieCarWillBeAllocatedAnyoneIfTheFirstOptionIsAllocated()
            => this.Given(s => s.TheTestModuleIsInitialised())
                .And(s => SalesPersonIsAvailable(TradeVehicleSpecialist))
                .And(s => ASalePersonHasBeenAllocated(TradeVehicleSpecialist))
                .And(s => SalesPersonIsAvailable(SportsCarSpecialist))
                .When(s => s.AnAllocationIsRequested(CustomerPreference(LanguagePreference.NoPreference, CarPreference.Tradie)))
                .Then(s => s.CustomerIsAllocatedTo(SportsCarSpecialist))
                .BDDfy();
    }
}
