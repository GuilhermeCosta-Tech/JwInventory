using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class AdminUser : BaseUser
    {
        public AdminUser(string name, string email, string passwordHash)
            : base(name, email, passwordHash, Enums.UserRole.Admin) 
        {
        }

        public bool CanAccessAllProducts() => true;
    }
}
