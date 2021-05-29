using Polystone.Business;
using Polystone.Services.Interfaces;

namespace Polystone.Services
{
    public class PolystoneContextService : IPolystoneContextService
    {
        private PolystoneContext _polystoneContext = new PolystoneContext();

        public PolystoneContext GetPolystoneContext()
        {
            return _polystoneContext;
        }
    }
}
