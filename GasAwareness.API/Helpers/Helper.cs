using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GasAwareness.API.Entities;

namespace GasAwareness.API.Helpers
{
    public static class Helper
    {
        public static bool IsInRole(this ClaimsPrincipal user, string[] roleNames)
        {
            if (user == null || !user.Identity.IsAuthenticated) return false;

            return roleNames.Any(x => user.IsInRole(x));
        }

        public static string? UserId(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string? Email(this ClaimsPrincipal user)
        {
            return user?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}