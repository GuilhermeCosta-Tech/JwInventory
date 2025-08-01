﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class EmployeeUser : BaseUser
    {
        public EmployeeUser(string name, string email, string passwordHash)
    : base(name, email, passwordHash, Enums.UserRole.Colaborador)
        {
        }

        public bool CanEditOwnProfile() => true;
    }
}
