using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Authentication.POC.Web.Client.AuthProviders
{
    public class TestAuthProviders : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Lester Feria"),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            var anonymous = new ClaimsIdentity(claims, "testAuthType");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}
