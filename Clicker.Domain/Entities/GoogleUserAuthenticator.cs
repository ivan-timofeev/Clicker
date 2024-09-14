using Clicker.Domain.Interfaces;

namespace Clicker.Domain.Entities;

public class GoogleUserAuthenticator : IUserAuthenticator
{
    public required string GoogleUserId { get; set; }
}
