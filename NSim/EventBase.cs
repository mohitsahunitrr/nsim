using System;
using System.Collections.Generic;

namespace NSim
{
    public abstract class EventBase : IEvent
    {
        private ICollection<Action> _callbacks;

        protected EventBase()
        {
            _callbacks = new List<Action>();
        }

        public virtual void Dispose()
        {
            _callbacks = null;
        }

        public virtual void Fire()
        {
            if (this.IsFired) throw new InvalidOperationException("event has already fired");

            IsFired = true;
            
            foreach (var item in _callbacks)
                item();

            _callbacks = null;
        }

        public bool IsFired { get; private set; }

        public virtual void Schedule(IContext c)
        {
        }

        public ICollection<Action> Callbacks {
            get { return _callbacks; }
        }
        
    }
}