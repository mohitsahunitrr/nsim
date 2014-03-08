using System;
using System.Collections.Generic;

namespace NSim
{
    public interface IContext
    {
        DateTime Step();
        void Run(TimeSpan @for);
        void Run(TimeSpan @for, TimeSpan interval);
        void Run(DateTime until);
        void Run(DateTime until, TimeSpan interval);
        DateTime Now { get; }
        void Schedule(IEvent e);
        void Schedule(IEvent e, TimeSpan delay);
        void Schedule(IEvent e, DateTime @at);
        IEvent Process(IEnumerable<IEvent> process);
        Random Random { get; }
    }
}