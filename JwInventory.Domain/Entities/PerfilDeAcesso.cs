using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class PerfilDeAcesso : IdentityRole<int>
    {
        public PerfilDeAcesso() : base()
        {
        }

        public PerfilDeAcesso(string roleName) : base(roleName)
        {
        }
    }
}
