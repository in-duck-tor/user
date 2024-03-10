using System.ComponentModel.DataAnnotations;

namespace InDuckTor.User.Domain
{
    public class Client 
    {
        public long Id { get; init; }

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

        public virtual User User { get; init; }
    }
}
