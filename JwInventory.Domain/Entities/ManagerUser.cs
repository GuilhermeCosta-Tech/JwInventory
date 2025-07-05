using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class ManagerUser : BaseUser
    {
        public ManagerUser(string name, string email, string passwordHash)
            : base(name, email, passwordHash, Enums.UserRole.Gerente)
        {
        }

        public bool CanViewInventoryReports() => true;
    }
}
