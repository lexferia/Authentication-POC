namespace Authentication.POC.API.APIKey.Common
{
    public class AuthenticationSettings
    {
        public struct APIKey 
        { 
            public string Key { get; set; }
            public string Owner { get; set; }
        }

        public string Realm { get; set; } = default!;
        public APIKey[] APIKeys { get; set; } = default!;
    }
}
