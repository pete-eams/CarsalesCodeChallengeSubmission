namespace SalesPersonAllocator.DomainLogic.Interfaces
{
    interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle();
    }
}
