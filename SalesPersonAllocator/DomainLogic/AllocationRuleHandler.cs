using System.Linq;
using System.Threading.Tasks;
using SalesPersonAllocator.DomainLogic.Interfaces;
using SalesPersonAllocator.DomainModels;

namespace SalesPersonAllocator.DomainLogic
{
    public class AllocationRuleHandler : IHandler
    {
        private const int SECOND_TO_MILLISECOND = 1000;
        private static readonly object Lock = new object ();  

        private IHandler _nextHandler;
        private readonly SalesPersonStore _store;
        private readonly SalesPersonCriteria _salesPersonCriteria;
        private readonly BehaviourConfiguration _behaviourConfiguration;

        public AllocationRuleHandler(
            SalesPersonStore store,
            SalesPersonCriteria salesPersonCriteria,
            BehaviourConfiguration behaviourConfiguration)
        {
            _store = store;
            _salesPersonCriteria = salesPersonCriteria;
            _behaviourConfiguration = behaviourConfiguration;
        }

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public object Handle(object _)
        {
            AllocatableSalesPerson matchingSalesPerson;
            lock (Lock) 
            {
                matchingSalesPerson = _store
                    .FindMatchingSalesPerson(SalesPersonMatchingCriteria)
                    .FirstOrDefault();

                matchingSalesPerson?.AllocateToCustomer();
            }

            SimulateDeAllocationIfRequired(matchingSalesPerson);
            return matchingSalesPerson ?? _nextHandler?.Handle(null);
        }

        private void SimulateDeAllocationIfRequired(AllocatableSalesPerson salesPerson)
        {
            if (!_behaviourConfiguration.DeAllocationTimeSec.HasValue)
                return;

            Task.Delay(_behaviourConfiguration.DeAllocationTimeSec.Value * SECOND_TO_MILLISECOND)
                .ContinueWith(t => salesPerson.Deallocate());
        }

    private bool SalesPersonMatchingCriteria(AllocatableSalesPerson salesPerson) 
            => _salesPersonCriteria.Matches(salesPerson);
    }
}
