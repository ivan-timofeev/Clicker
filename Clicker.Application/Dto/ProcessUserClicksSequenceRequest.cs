using Clicker.Domain.Entities;

namespace Clicker.Application.Dto;

public class ProcessUserClicksSequenceRequest
{
    public required User User { get; init; }
    public required int UserClicks { get; init; }
    
    public class UserClick
    {
        public required DateTime ClickDateTimeUtc { get; init; }
    }
}
