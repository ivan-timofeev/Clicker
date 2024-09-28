using Clicker.Domain.Interfaces;

namespace Clicker.Domain.Entities;

public class GoogleUserAuthenticator : IUserAuthenticator
{
    public string GoogleUserId { get; private set; }
    
    public GoogleUserAuthenticator(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        GoogleUserId = googleUserId;
    }

    public void SetGoogleUserId(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        GoogleUserId = googleUserId;
    }
}
