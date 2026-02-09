using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasAwareness.API.Enums
{
    public static class RoleGroup
    {
        public static string[] AllRoles = new string[] { RoleName.Admin, RoleName.Editor, RoleName.Visitor };
        public static string[] AdminAndEditor = new string[] { RoleName.Admin, RoleName.Editor };
    }
}