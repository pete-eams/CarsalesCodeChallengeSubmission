using System;
using System.Collections.Generic;
using System.Threading;
using SalesPersonAllocator.Infrastructure.Interfaces;

namespace SalesPersonAllocator.Infrastructure
{
    public class Dispatcher : ITaskReceiver, IDisposable
    {
        private bool _isRunning;
        private readonly Thread _dispatcherThread;
        private readonly Queue<Action> _taskQueue;

        public Dispatcher()
        {
            _isRunning = true;
            _taskQueue = new Queue<Action>();
            _dispatcherThread = new Thread(ExecuteRequestedTasks) { Name = "Dispatcher Thread" };
        }

        public void ExecuteRequestedTasks()
        {
            while (_isRunning)
                ExecuteActionsFromQueue();
        }

        public void AddTask(Action action) 
            => _taskQueue.Enqueue(action);
        
        private void ExecuteActionsFromQueue()
        {
            if (_taskQueue.TryDequeue(out var action))
                action.Invoke();
        }

        public void Dispose()
        {
            if (_isRunning)
                _isRunning = false;

            _dispatcherThread.Join(100);
        }
    }
}
