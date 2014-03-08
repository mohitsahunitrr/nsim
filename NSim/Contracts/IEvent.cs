using System;
using System.Collections.Generic;

namespace NSim
{
    public interface IEvent<T> : IEvent
    {
        void Fire(T obj);
        new ICollection<Action<T>> Callbacks { get; }
        T Result { get; }
        
    }

    public interface IEvent : IDisposable
    {
        //IContext Context { get; }
        //EventState State { get; }
        //void Succeed();
        //void Fail(Exception e);
        void Fire();
        bool IsFired { get; }
        void Schedule(IContext c);
        ICollection<Action> Callbacks { get; }
    }
}