using Polystone.Business;
using Polystone.Services.Interfaces;

namespace Polystone.Services
{
    public class PolystoneContextService : IPolystoneContextService
    {
        private readonly PolystoneContext _polystoneContext = new PolystoneContext();

        public PolystoneContext GetPolystoneContext()
        {
            return _polystoneContext;
        }
    }
}
