using System;
using System.Collections.Generic;

namespace NSim
{
    public class RequestEvent : EventBase
    {
        private readonly IResource _owner;

        public bool IsReleased { get; private set; }
        public IResource Owner { get { return _owner; } }

        internal void Release()
        {
            IsReleased = true;
        }

        internal RequestEvent(IResource owner)
        {
            _owner = owner;
        }

        public override void Dispose()
        {
            _owner.Release(this);
            base.Dispose();
        }

        public override void Schedule(IContext c)
        {
            
        }
    }
}