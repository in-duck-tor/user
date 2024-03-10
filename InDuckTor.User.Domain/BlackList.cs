namespace InDuckTor.User.Domain
{
    public class BlackList
    {
        public long Id { get; init; }
        public long UserId { get; init; }
        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public DateTime EndAt { get; init; }
        public string? Reason { get; init; }
    }
}
