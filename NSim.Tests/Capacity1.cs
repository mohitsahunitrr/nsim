using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NSim.Tests
{
    [TestFixture]
    public class Capacity1
    {
        private IContext _context;
        private IContinuousContainer _fuelTank;
        private IResource _gasStation;

        [Test]
        public void Test1()
        {
            _context = new SimContext();
            _fuelTank = new SimpleContinousContainer(1000, 500);
            _gasStation = new SimpleResource(1);

            _context.Process(GasStationControl());
            _context.Process(CarGenerator());

            for (int i = 0; i < 5000; i++)
            {
                _context.Step();
            }
        }

        private IEnumerable<IEvent> Car(int i)
        {
            Trace.WriteLine("Created car " + i + " at " + _context.Now);
            IEvent req;
            

            using ((req = _gasStation.Request()))
            {
                var start = _context.Now;

                yield return req;

                var litresRequired = 50*_context.Random.NextDouble();

                yield return _fuelTank.Get(litresRequired);

                double newLEvel = _fuelTank.Level;

                yield return new Timeout((litresRequired/0.4).Seconds());

                Trace.WriteLine(string.Format("{0} finished refueling {2}L in {1} at {3} leaving {4}", i, _context.Now - start, litresRequired, _context.Now, newLEvel));
            }
        }

        private IEnumerable<IEvent> CarGenerator()
        {
            for (int i=0;;i++)
            {
                yield return new Timeout((_context.Random.Uniform(30, 300)).Seconds());
                _context.Process(Car(i));
            }
        }

        private IEnumerable<IEvent> GasStationControl()
        {
            for (;;)
            {
                if (_fuelTank.Level/_fuelTank.Capacity < 0.1)
                {
                    Console.WriteLine("**Calling tanker at " + _context.Now);
                    yield return _context.Process(FuelTruck());
                }
                yield return new Timeout(10.Seconds());
            }
        }

        public IEnumerable<IEvent> FuelTruck()
        {
            yield return new Timeout(30.Minutes());
            Trace.WriteLine("**Tank truck arriving at " + _context.Now);
            var amount = _fuelTank.Capacity - _fuelTank.Level;
            Trace.WriteLine(string.Format("**Tank truck refuelling {0} liters.", amount));
            yield return _fuelTank.Put(amount);
        }
    }
}
