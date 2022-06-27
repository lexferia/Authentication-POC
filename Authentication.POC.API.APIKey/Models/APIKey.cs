using AspNetCore.Authentication.ApiKey;
using System.Security.Claims;

namespace Authentication.POC.API.APIKey.Models
{
    public class APIKey : IApiKey
    {
        public APIKey(string key, string ownerName, IReadOnlyCollection<Claim>? claims = null)
        { 
            Key = key;
            OwnerName = ownerName;
            Claims = claims ?? new List<Claim>();
        }

        public string Key { get; }

        public string OwnerName { get; }

        public IReadOnlyCollection<Claim> Claims { get; }
    }
}
