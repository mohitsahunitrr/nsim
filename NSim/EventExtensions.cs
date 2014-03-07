using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public static class EventExtensions
    {
        public static IEvent Or(this IEvent a, IEvent b)
        {
            if (a.IsFired)
                return a;
            else if (b.IsFired)
                return b;
            else
                return new OrEvent(a, b);
        }
    }
}
