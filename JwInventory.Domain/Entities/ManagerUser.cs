using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class ManagerUser : PessoaComAcesso
    {
        public ManagerUser() { }

        public ManagerUser(string name, string email)
        {
            UserName = name;
            Email = email;
        }

        public bool CanManageProducts() => true;
    }
}
