﻿using Clicker.Domain.Interfaces;

namespace Clicker.Domain.Entities;

public class User
{
    public required string Id { get; init; }
    public required string Login { get; set; }
    public required decimal Balance { get; set; }

    public required UserPersonalData UserPersonalData { get; set; }
        = new UserPersonalData();

    public IList<IUserAuthenticator> Authenticators { get; set; }
        = new List<IUserAuthenticator>();
}
