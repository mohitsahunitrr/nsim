using System;

namespace NSim
{
    public sealed class OrEvent : DataEventBase<IEvent>
    {
        private readonly IEvent _a;
        private readonly IEvent _b;

        

        internal OrEvent(IEvent a, IEvent b)
        {
            if (_a.IsFired)
            {
                Result = a;
                IsFired = true;
                _callbacks = null;
                return;
            }

            if (_b.IsFired)
            {
                Result = b;
                IsFired = true;
                _callbacks = null;
                return;
            }

            _a = a;
            _b = b;
            a.Callbacks.Add(() => Callback(a));
            b.Callbacks.Add(() => Callback(b));
        }

        private void Callback(IEvent e)
        {
            if (!this.IsFired)
                Fire(e);
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