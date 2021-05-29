using Polystone.Business;

namespace Polystone.Services.Interfaces
{
    public interface IPolystoneServerService
    {
        public void Start(int port);

        public PolystoneServer GetPolystoneServer();
    }
}
