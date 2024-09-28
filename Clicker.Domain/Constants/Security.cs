namespace Clicker.Domain.Constants;

public static class Security
{
    public static class OAuth
    {
        public const string UserIdClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public const string EmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string NameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    }
}
