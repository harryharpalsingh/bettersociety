using System.Security.Claims;

namespace bettersociety.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            // Ensure user and claims are not null
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claim = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).Value;

            // Return the claim value or an empty string if the claim is not found
            return claim ?? string.Empty;
        }
    }
}
