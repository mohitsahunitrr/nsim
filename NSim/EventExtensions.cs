using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public static class EventExtensions
    {
        public static OrEvent Or(this IEvent a, IEvent b)
        {
            return new OrEvent(a, b);
        }
    }
}
