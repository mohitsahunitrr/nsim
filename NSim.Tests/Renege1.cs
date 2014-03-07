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
    public class Renege1
    {
        private SimContext _context;
        private SimpleResource _counter;
        private double _timeInBank = 12;
        private int _minPatience = 5;
        private int _maxPatience = 5;

        [Test]
        public void A()
        {
            _context = new SimContext();
            _counter = new SimpleResource(2);
            _context.Process(CustomerGenerator(2, 100));

            for (int i = 0; i < 30; i++)
            {
                _context.Step();
            }

            //todo
        }

        private IEnumerable<IEvent> CustomerGenerator(double everySeconds, int total)
        {
            for (int i = 0;i<total; i++)
            {
                yield return new Timeout(_context.Random.Exponential(1/everySeconds).Seconds());
                Trace.WriteLine("Generated customer " + i + " at " + _context.Now);
                _context.Process(Customer(i));
            }

        }

        private IEnumerable<IEvent> Customer(int customerId)
        {
            RequestEvent req;
            DateTime arrive = _context.Now;
            
            using (req = _counter.Request())
            {
                yield return new Timeout(_context.Random.Uniform(_minPatience, _maxPatience).Seconds()).Or(req);

                if (req.IsFired)
                {
                    yield return new Timeout(_timeInBank.Seconds());
                    Trace.WriteLine("Customer " + customerId + " finished at " + _context.Now + " after waiting for " + (_context.Now - arrive));
                }
                else
                {
                    Trace.WriteLine("Customer " + customerId + " reneged at " + _context.Now + " after waiting for " + (_context.Now - arrive));
                }
            }
        }
    }

}
