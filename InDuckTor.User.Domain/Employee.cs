

using System.ComponentModel.DataAnnotations;

namespace InDuckTor.User.Domain
{
    public class Employee 
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

        public string? Email { get; init; }

        public required string FirstName { get; init; }

        public required string LastName { get; init; }

        public string? MiddleName { get; init; }

        public DateTime? BirthDate { get; init; }

        /// <summary>
        /// Должность
        /// </summary>
        public List<string> Position { get; set; } = new();

        /// <summary>
        /// Ключи разрешения на совершение операций
        /// </summary>
        public List<Permission> Permissions { get; set; } = new();

        public virtual User User {  get; init; }
    }
}
