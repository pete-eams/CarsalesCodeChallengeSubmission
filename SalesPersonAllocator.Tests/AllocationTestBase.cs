using System.Linq;
using SalesPersonAllocator.DomainModels;
using SalesPersonAllocatorTest.TestInfrastructure;
using Xunit;

namespace SalesPersonAllocator.Tests
{
    public class AllocationTestBase
    {
        private SalesPerson _salesPersonResult;
        private readonly TestModule _testModule;

        public AllocationTestBase()
        {
            _testModule = new TestModule();
        }

        protected void TheTestModuleIsInitialised()
            => _testModule.Init();

        protected void SalesPersonIsAvailable(AllocatableSalesPerson salesPerson) =>
            _testModule.SalesPersonStore.RegisterNewSalesPerson(salesPerson);

        protected static void SalesPersonDataIsEmpty() { }
        
        protected CustomerPreference CustomerPreference(LanguagePreference lang, CarPreference car) =>
            new CustomerPreference(lang, car);
        
        protected void AnAllocationIsRequested(CustomerPreference customerPreference)
            => _salesPersonResult = _testModule.Allocator.GetAllocation(customerPreference);

        protected static SalesPerson InvalidSalesPerson => new InvalidSalesPerson();

        protected static AllocatableSalesPerson GreekAndSportsCarSpecialist => new AllocatableSalesPerson(
            "James", SalesGroup.SpeaksGreek, SalesGroup.SportsCarSpecialist);
        
        protected static AllocatableSalesPerson GreekAndFamilyCarSpecialist => new AllocatableSalesPerson(
            "David", SalesGroup.SpeaksGreek, SalesGroup.FamilyCarSpecialist);

        protected static AllocatableSalesPerson TradeVehicleSpecialist => new AllocatableSalesPerson(
            "John", SalesGroup.TradeVehicleSpecialist);

        protected static AllocatableSalesPerson SportsCarSpecialist => new AllocatableSalesPerson(
            "John", SalesGroup.SportsCarSpecialist);

        protected static AllocatableSalesPerson FamilyCarSpecialist => new AllocatableSalesPerson(
            "Kim", SalesGroup.FamilyCarSpecialist);

        protected void CustomerIsAllocatedTo(SalesPerson salesPerson)
            => Assert.Equal(salesPerson, _salesPersonResult);

        protected void AllSalesPersonAreAllocated()
        {
            var salesPeople = _testModule.SalesPersonStore.FindMatchingSalesPerson(Any);
            foreach (var salesPerson in salesPeople)
            {
                salesPerson.AllocateToCustomer();
            }
        }

        protected void ASalePersonHasBeenAllocated(AllocatableSalesPerson salesPerson) 
            => _testModule.SalesPersonStore.FindMatchingSalesPerson(
                s => s.Equals(salesPerson)).Single().AllocateToCustomer();
        
        private static bool Any(SalesPerson s) => true;
    }
}
