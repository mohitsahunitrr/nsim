using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public class SimContext : IContext
    {
        private DateTime _simTime;

        private MinHeap<IEvent> _heap = new MinHeap<IEvent>();

        public SimContext()
        {
        }

        public DateTime Step()
        {
            if (_heap.Count == 0)
                throw new ScheduleEmptyException();
            KeyValuePair<long, IEvent> next = _heap.Pop();
            _simTime = new DateTime(next.Key);

            ProcessEvent(next.Value);

            return _simTime;

        }

        private void ProcessEvent(IEvent value)
        {
            if (value == null) throw new ArgumentNullException("e");
            if (value.Context != this) throw new InvalidOperationException("event doens't belong to this context");

            foreach (var callback in value.Callbacks)
                callback();

            value.Dispose();
        }

        public void Run(TimeSpan @for)
        {
            throw new NotImplementedException();
        }

        public void Run(DateTime until)
        {
            throw new NotImplementedException();
        }

        public DateTime Now { get { return _simTime; } }

        public void Schedule(IEvent e)
        {
            if (e == null) throw new ArgumentNullException("e");
            if (e.Context != this) throw new InvalidOperationException("event doens't belong to this context");
            _heap.Add(_simTime.Ticks, e);
        }

        public void Schedule(IEvent e, int priority)
        {
            throw new NotImplementedException();
        }

        public void Schedule(IEvent e, TimeSpan delay)
        {
            throw new NotImplementedException();
        }

        public void Schedule(IEvent e, int priority, TimeSpan delay)
        {
            throw new NotImplementedException();
        }

        public void Schedule(IEvent e, DateTime at)
        {
            if (e == null) throw new ArgumentNullException("e");
            if (e.Context != this) throw new InvalidOperationException("event doens't belong to this context");
            _heap.Add(at.Ticks, e);
        }

        public void Process(IEnumerable<IEvent> process)
        {
            if (process == null) throw new ArgumentNullException("process");
            Process(process.GetEnumerator());
        }

        private void Process(IEnumerator<IEvent> enumerator)
        {
            if (enumerator.MoveNext())
            {
                enumerator.Current.Callbacks.Add(() => Process(enumerator));
            }
        }

        public void Schedule(IEvent e, int priority, DateTime at)
        {
            throw new NotImplementedException();
        }
    }
}
