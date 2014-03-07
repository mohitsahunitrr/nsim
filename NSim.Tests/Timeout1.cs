using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NSim.Tests
{
    [TestFixture]
    public class Timeout1
    {
        private DateTime _val;

        [Test]
        public void TestMethod1()
        {
            var c = new SimContext();
            c.Process(Clock(c, 2.Seconds()));
            c.Process(Clock(c, 3.Seconds()));
            c.Step();
            Assert.True(_val == 2.Seconds().From(new DateTime(0)));
            c.Step();
            Assert.True(_val == 3.Seconds().From(new DateTime(0)));
            c.Step();
            Assert.True(_val == 4.Seconds().From(new DateTime(0)));
            c.Step();
            Assert.True(_val == 6.Seconds().From(new DateTime(0)));
            c.Step();
            Assert.True(_val == 6.Seconds().From(new DateTime(0)));
        }

        IEnumerable<IEvent> Clock(SimContext e, TimeSpan ts)
        {
            for (;;)
            {
                yield return new Timeout(ts);
                _val = e.Now;
            }
        }
    }

    


}
