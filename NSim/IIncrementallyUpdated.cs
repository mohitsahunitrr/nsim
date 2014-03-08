using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public interface IIncrementallyUpdated
    {
        void AdvanceBy(TimeSpan t, SimContext context);
    }
}
