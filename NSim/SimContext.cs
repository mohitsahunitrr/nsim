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

        private readonly MinHeap<IEvent> _heap = new MinHeap<IEvent>();
        private readonly Random _random = new Random();
        private readonly List<IIncrementallyUpdated> _incrementalUpdateSet = new List<IIncrementallyUpdated>();

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


            value.Fire();

            value.Dispose();
        }

        public void Run(TimeSpan @for)
        {
            throw new NotImplementedException();
        }

        public void Run(TimeSpan @for, TimeSpan interval)
        {
            throw new NotImplementedException();
        }

        public void Run(DateTime until)
        {
            while (_simTime <= until)
            {
                Step();
            }
        }

        public void Run(DateTime until, TimeSpan interval)
        {
            if (_heap.Count == 0)
                throw new ScheduleEmptyException();

            //todo: when there is nothign in the update set, can skip to the next event in the schedule

            DateTime newTime;
            while ((newTime = Now + interval) <= until)
            {
                //process relevant events in the schedule
                while (_heap.MinimumKey() <= newTime.Ticks)
                {
                    var nextEvent = _heap.Pop();
                    _simTime = new DateTime(nextEvent.Key);
                    ProcessEvent(nextEvent.Value);
                }

                //run updaters
                _simTime = newTime;

                foreach (var item in _incrementalUpdateSet)
                {
                    item.AdvanceBy(interval, this);
                }
            }
        }

        public DateTime Now { get { return _simTime; } }

        public void Schedule(IEvent e)
        {
            if (e == null) throw new ArgumentNullException("e");
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
            _heap.Add(at.Ticks, e);
        }

        public IEvent Process(IEnumerable<IEvent> process)
        {
            if (process == null) throw new ArgumentNullException("process");
            var e = new ProcessCompletionEvent();
            Process(process.GetEnumerator(),e );
            return e;
        }

        public Random Random { get { return _random; } }

        private void Process(IEnumerator<IEvent> enumerator, ProcessCompletionEvent e, IIncrementallyUpdated last = null)
        {
            if (last != null)
            {
                _incrementalUpdateSet.Remove(last);
            }
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                var updateOnStep = enumerator.Current as IIncrementallyUpdated;
                if (!item.IsFired)
                {
                    item.Callbacks.Add(() => Process(enumerator, e, updateOnStep));
                    item.Schedule(this);
                    if (updateOnStep != null)
                    {
                        _incrementalUpdateSet.Add(updateOnStep); 
                    }
                    return;
                }
            }

            if(!e.IsFired)  //todo
                e.Fire();
        }

        public void Schedule(IEvent e, int priority, DateTime at)
        {
            throw new NotImplementedException();
        }
    }
}
