using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InDuckTor.User.Domain
{
    public class User
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

        public List<BlackList> Bans { get; set; } = new();
    }

    public enum AccountType
    {
        Employee,

        Client,
    }
}
