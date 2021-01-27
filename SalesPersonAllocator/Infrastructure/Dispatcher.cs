using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesPersonAllocator.Infrastructure.Interfaces;

namespace SalesPersonAllocator.Infrastructure
{
    public class Dispatcher : ITaskExecutor, ITaskReceiver
    {
        private bool _running;
        private readonly Queue<Action> _taskQueue;

        public Dispatcher()
        {
            _running = false;
            _taskQueue = new Queue<Action>();
        }

        public void Run()
        {
            SetCanExecuteActions();
            
            while (CanExecuteActions())
                ExecuteActionsFromQueue();
        }
        
        public void Stop() 
            => _running = false; 

        public void AddTask(Action action) 
            => _taskQueue.Enqueue(action);

        private void SetCanExecuteActions() 
            => _running = true;

        private bool CanExecuteActions() 
            => _running;
        
        private void ExecuteActionsFromQueue()
        {
            if (_taskQueue.TryDequeue(out var action))
                action.Invoke();
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
