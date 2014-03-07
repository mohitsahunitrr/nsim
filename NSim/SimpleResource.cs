using System;
using System.Collections.Generic;

namespace NSim
{
    public class SimpleResource : IResource
    {
        private readonly LinkedList<RequestEvent> _waiting = new LinkedList<RequestEvent>();
        private readonly int _capacity;
        private int _using;

        public SimpleResource(int capacity)
        {
            _capacity = capacity;
        }

        public RequestEvent Request()
        {
            var e = new RequestEvent(this);
            
            if (_using < _capacity)
            {
                _using++;
                e.Fire();
            }
            else
            {
                _waiting.AddLast(e);
            }
            return e;
        }

        public void Release(RequestEvent @event)
        {
            if (@event.Owner != this)
                throw new InvalidOperationException("not owned by this");

            if (@event.IsReleased)
                return;

            @event.Release();

            if (!@event.IsFired) //reneg
            {
                _waiting.Remove(@event);
                return;
            }

            _using--;

            if (_waiting.Count > 0)
            {
                RequestEvent first = _waiting.First.Value;
                _waiting.RemoveFirst();
                _using++;
                first.Fire();
            }
        }
    }
}