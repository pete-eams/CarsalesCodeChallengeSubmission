namespace SalesPersonAllocator.DomainModels
{
    public class AllocatableSalesPerson : SalesPerson
    {
        public AllocatableSalesPerson(
            string name, params SalesGroup[] groups) 
            : base(name, groups)
        {
        }
        
        public bool IsAllocated { get; private set; }

        public void AllocateToCustomer()
            => IsAllocated = true;
    }
}