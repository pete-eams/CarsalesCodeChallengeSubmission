using System.Collections.Generic;
using System.Linq;
using DomainLogic.DomainLogic.Interfaces;

namespace DomainLogic.DomainLogic.Utilities
{
    public abstract class HandlerBuilder
    {
        private readonly List<IHandler> _handlers;

        protected HandlerBuilder()
        {
            _handlers = new List<IHandler>();
        }

        protected void AddHandler(IHandler handler)
            => _handlers.Add(handler);

        public IHandler Build()
        {
            // linking the handlers according to the "chain of responsibility"
            for (var i = 0; i < _handlers.Count - 1; i++)
            {
                var currentHandler = _handlers[i];
                var nextHandler = _handlers[i + 1];

                currentHandler.SetNext(nextHandler);
            }

            return _handlers.First();
        }
    }
}