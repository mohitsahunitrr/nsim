using System;
using System.Collections.Generic;

namespace NSim
{
    public class SimpleContinousContainer : IContinuousContainer
    {
        private readonly LinkedList<ContinuousContainerRequestEvent> _gets = new LinkedList<ContinuousContainerRequestEvent>();
        private readonly LinkedList<ContinuousContainerRequestEvent> _puts = new LinkedList<ContinuousContainerRequestEvent>();
        private readonly double _capacity;
        private double _using;

        public SimpleContinousContainer(double capacity, double initial)
        {
            if (capacity < 0)
                throw new ArgumentException("capacity");
            if (initial < 0 || initial > capacity)
                throw new ArgumentException("initial");
            _capacity = capacity;
            _using = initial;
        }

        public ContinuousContainerRequestEvent Get(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("amount");

            var e = new ContinuousContainerRequestEvent(this, amount);

            if (amount <= _using)
            {
                _using -= amount;
                e.Fire();
                Pops();
            }
            else
            {
                _gets.AddLast(e);
            }
            return e;
        }

        private void Pops()
        {
            for (;;)
            {
                ContinuousContainerRequestEvent first;
                if (_gets.Count > 0 && (first = _gets.First.Value).Amount <= _using)
                {                     
                    _gets.RemoveFirst();
                    _using -= first.Amount;
                    first.Fire();
                    continue;
                }
                if (_puts.Count > 0 && (first = _gets.First.Value).Amount + _using <= _capacity)
                {
                    _puts.RemoveFirst();
                    _using -= first.Amount;
                    first.Fire();
                    continue;
                }
                break;
            }
        }


        public ContinuousContainerRequestEvent Put(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("amount");

            var e = new ContinuousContainerRequestEvent(this, amount);

            if ( _using + amount <= _capacity)
            {
                _using += amount;
                e.Fire();
                Pops();
            }
            else
            {
                _puts.AddLast(e);
            }
            return e;
        }

        public double Capacity {
            get { return _capacity; }
        }
        public double Level { get { return _using; } }
    }
}