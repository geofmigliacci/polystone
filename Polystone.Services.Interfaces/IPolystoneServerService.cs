using Polystone.Business;
using Polystone.Business.Models;

namespace Polystone.Services.Interfaces
{
    public interface IPolystoneAccountService
    {
        public void SetAccount(Account account);
        public Account GetAccount();
        public bool HasAccount();
    }
}
