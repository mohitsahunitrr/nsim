using System;
using System.Collections.Generic;

namespace NSim
{
    public interface IContext
    {
        DateTime Step();
        void Run(TimeSpan @for);
        void Run(DateTime @until);
        DateTime Now { get; }
        void Schedule(IEvent e);
        void Schedule(IEvent e, TimeSpan delay);
        void Schedule(IEvent e, DateTime @at);
        IEvent Process(IEnumerable<IEvent> process);
        Random Random { get; }
    }
}