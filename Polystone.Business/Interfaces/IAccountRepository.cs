using Polystone.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Polystone.Business.Interfaces
{
    public interface IAccountRepository
    {
        public Account GetByName(string name);
        public Account CreateAccountIfNotExist(Payload payload);
    }
}
