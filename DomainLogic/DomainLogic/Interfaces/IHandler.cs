﻿namespace DomainLogic.DomainLogic.Interfaces
{
    public interface IHandler // interface for implementing chain of responsibility pattern
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }
}
