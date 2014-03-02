using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public class Timeout : IEvent
    {
        private ICollection<Action> _callbacks;
        private IContext _context;

        public Timeout(SimContext c, TimeSpan amt)
        {
            _context = c;
            c.Schedule(this, c.Now.Add(amt));
            _callbacks = new List<Action>();
        }

        public void Dispose()
        {
            _callbacks = null;
        }

        public IContext Context { get { return _context; } }

        public EventState State { get; private set; }

        public void Succeed()
        {
            throw new NotImplementedException();
        }

        public void Fail(Exception e)
        {
            throw new NotImplementedException();
        }

        public ICollection<Action> Callbacks { get { return _callbacks; } }
    }
}
