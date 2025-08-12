using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class AdminUser : PessoaComAcesso
    {
        public AdminUser()
        {
            
        }
        public AdminUser(string name, string email, string passwordHash)
        {
            UserName = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public bool CanAccessAllProducts() => true;
    }
}
