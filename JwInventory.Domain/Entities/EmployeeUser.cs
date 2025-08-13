using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class EmployeeUser : PessoaComAcesso
    {
        public EmployeeUser() { }

        public EmployeeUser(string name, string email)
        {
            UserName = name;
            Email = email;
        }

        public bool CanViewProducts() => true;
    }
}
