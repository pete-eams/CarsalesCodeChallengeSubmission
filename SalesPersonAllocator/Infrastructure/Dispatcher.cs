using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    public static class DispatcherExtension
    {
        public static Task<IActionResult> AddHttpTask<T>(
            this ITaskReceiver taskReceiver,
            Func<T> func)
        {
            var tcs = new TaskCompletionSource<IActionResult>();
            taskReceiver.AddTask(SetTaskCompletionResult(tcs, func));

            return WaitForTaskCompletion(tcs);
        }

        private static Action SetTaskCompletionResult<T>(
            TaskCompletionSource<IActionResult> tcs, 
            Func<T> func)
        {
            return () =>
            {
                try
                {
                    tcs.SetResult(Result(func).Invoke());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            };
        }

        private static async Task<IActionResult> WaitForTaskCompletion(
            TaskCompletionSource<IActionResult> tcs)
        {
            try
            {
                return await tcs.Task;
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        private static Func<IActionResult> Result<T>(this Func<T> func) 
            => () => new OkObjectResult(func);
    }
}
