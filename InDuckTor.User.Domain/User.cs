using System.Runtime.Serialization;

namespace InDuckTor.User.Domain
{
    public class User
    {
        public long Id { get; set; }

        public required string Login { get; init; }

        public string? Email { get; init; }

        public required string FirstName { get; init; }

        public required string LastName { get; init; }

        public string? MiddleName { get; init; }

        public DateTime? BirthDate { get; init; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата блокировки учётной записи
        /// </summary>
        public DateTime? InactiveAt { get; init; }

        public List<BlackList> Bans { get; set; } = new();

        public virtual Client? Client { get; set; }

        public virtual Employee? Employee { get; set; }
    }

    public enum Role
    {
        [EnumMember(Value = "employee")]
        Employee,
        [EnumMember(Value = "client")]
        Client,
    }
}
