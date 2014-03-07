using System;
using System.Collections.Generic;
using System.Linq;

namespace NSim
{
    public abstract class DataEventBase<T> : IEvent<T>
    {
        private ICollection<Action<T>> _callbacks;

        protected DataEventBase()
        {
            _callbacks = new List<Action<T>>();
        }

        public virtual void Dispose()
        {
            _callbacks = null;
        }

        public virtual void Schedule(IContext c)
        {
            c.Schedule(this);
        }

        public ICollection<Action<T>> Callbacks { get {return _callbacks; } }

        ICollection<Action> IEvent.Callbacks {
            get { return _callbacks.Select(x => new Action(() => x(default(T)))).ToArray(); }
        }


        public virtual T Result { get; protected set; }

        void IEvent.Fire()
        {
            Succeed(default(T));
        }

        public virtual bool IsFired { get; protected set; }

        public virtual void Succeed(T obj)
        {
            if (this.IsFired) throw new InvalidOperationException("event has already fired");

            IsFired = true;

            foreach (var item in _callbacks)
                item(obj);

            IsFired = true;
            Result = obj;
        }

    }
}