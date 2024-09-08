namespace Clicker.Domain.Entities;

public class User
{
    public required string Id { get; init; }
    public required string Login { get; set; }
    public required decimal Balance { get; set; }
}
