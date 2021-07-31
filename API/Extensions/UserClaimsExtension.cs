using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class UserClaimsExtension
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var idString = user.Claims.FirstOrDefault(claim=> claim.Type == "userId").Value;

            return int.Parse(idString);
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            var userName = user.Claims.FirstOrDefault(claim=> claim.Type == "userName").Value;

            return userName;
        }
    }
}