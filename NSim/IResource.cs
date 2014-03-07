using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSim
{
    public interface IResource
    {
        RequestEvent Request();
        void Release(RequestEvent @event);
    }
}
