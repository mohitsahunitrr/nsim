using System;

namespace NSim
{
    public class OrEvent : EventBase
    {
        private readonly IEvent _a;
        private readonly IEvent _b;

        internal OrEvent(IEvent a, IEvent b)
        {
            _a = a;
            _b = b;
            a.Callbacks.Add(Callback);
            b.Callbacks.Add(Callback);
        }

        private void Callback()
        {
            if (!this.IsFired)
                Fire();
        }

        public override void Schedule(IContext c)
        {
            if (this.IsFired)
                throw new InvalidOperationException("already fired");

            _a.Schedule(c);
            _b.Schedule(c);
        }
    }
}