using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public class Timeout : EventBase
    {
        private readonly TimeSpan _amt;

        public Timeout(TimeSpan amt)
        {
            _amt = amt;
        }

        public override  void Schedule(IContext c)
        {
            c.Schedule(this, c.Now + _amt);
        }

    }
}
