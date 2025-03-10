﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Auth
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public HasRoleRequirement(string role)
        {
            Role = role;
        }
    }
}
