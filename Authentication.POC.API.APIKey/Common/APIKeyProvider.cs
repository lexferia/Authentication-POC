using AspNetCore.Authentication.ApiKey;
using Models = Authentication.POC.API.APIKey.Models;

namespace Authentication.POC.API.APIKey.Common
{
    public class APIKeyProvider : IApiKeyProvider
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public APIKeyProvider(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        public Task<IApiKey?> ProvideAsync(string key)
        {
            if (_authenticationSettings is null || (!_authenticationSettings.APIKeys?.Any() ?? true))
            {
                throw new NotImplementedException("APIKeys not set");
            }

            _authenticationSettings.APIKeys ??= new AuthenticationSettings.APIKey[1];

            IEnumerable<IApiKey> apiKeys = _authenticationSettings.APIKeys
                                                                  .Select(item => new Models.APIKey(item.Key, item.Owner));

            return Task.FromResult(apiKeys.FirstOrDefault(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase)));
        }
    }
}
