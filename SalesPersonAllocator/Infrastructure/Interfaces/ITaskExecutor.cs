namespace SalesPersonAllocator.Infrastructure.Interfaces
{
    interface ITaskExecutor
    {
        void Run();

        void Stop();
    }
}
