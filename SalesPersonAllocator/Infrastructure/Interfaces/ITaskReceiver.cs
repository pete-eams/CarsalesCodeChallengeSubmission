using System;

namespace SalesPersonAllocator.Infrastructure.Interfaces
{
    public interface ITaskReceiver
    {
        void AddTask(Action action);
    }
}
