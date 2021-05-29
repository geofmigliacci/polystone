using Microsoft.EntityFrameworkCore;
using Polystone.Business.Interfaces;
using Polystone.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polystone.Business.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(PolystoneContext context) : base(context)
        {

        }
        public Account GetByName(string name)
        {
            return _context.Accounts.Include(a_ => a_.CurrentHistory).FirstOrDefault(a_ => a_.Name == name);
        }

        public Account CreateAccountIfNotExist(Payload payload)
        {
            Account currentAccount = GetByName(payload.AccountName);
            if (currentAccount != null)
            {
                return currentAccount;
            }

            Account newAccount = new Account()
            {
                Name = payload.AccountName,
                LastUpdateDate = DateTime.UtcNow
            };
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            AccountHistory accountHistory = new AccountHistory()
            {
                CreationDate = DateTime.UtcNow,
                Level = Convert.ToInt32(payload.Level),
                Longitude = payload.Lng,
                Latitude = payload.Lat,
                Account = newAccount
            };
            newAccount.CurrentHistory = accountHistory;
            _context.SaveChanges();

            return GetByName(payload.AccountName);
        }
    }
}
