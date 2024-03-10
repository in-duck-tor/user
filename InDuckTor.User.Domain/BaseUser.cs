using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InDuckTor.User.Domain
{
    public abstract class BaseUser
    {
        public long Id { get; init; }

        public required string Login { get; init; }

        public required AccountType AccountType { get; init; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата блокировки учётной записи
        /// </summary>
        public DateTime? InactiveAt { get; init; }

        [EmailAddress]
        public string? Email { get; init; }

        public required string FirstName { get; init; }

        public required string LastName { get; init; }

        public string? MiddleName { get; init; }

        public DateTime? BirthDate { get; init; }
    }

    public enum AccountType
    {
        [EnumMember(Value = "employee")] Employee,

        [EnumMember(Value = "client")] Client,
    }
}
