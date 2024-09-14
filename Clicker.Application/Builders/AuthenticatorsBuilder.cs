using Clicker.Domain.Entities;
using Clicker.Domain.Interfaces;

namespace Clicker.Application.Builders;

public class AuthenticatorsBuilder
{
    private string? _googleUserId;
    
    public AuthenticatorsBuilder AddGoogleAuthenticator(string googleUserId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(googleUserId);
        _googleUserId = googleUserId;
        return this;
    }

    public IList<IUserAuthenticator> Build()
    {
        var result = new List<IUserAuthenticator>();

        if (_googleUserId != null)
        {
            result.Add(new GoogleUserAuthenticator { GoogleUserId = _googleUserId });
        }

        return result;
    }
}