namespace SalesPersonAllocator.DomainLogic.Interfaces
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle();
    }
}
