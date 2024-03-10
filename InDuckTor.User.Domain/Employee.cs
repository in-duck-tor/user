

namespace InDuckTor.User.Domain
{
    public class Employee : BaseUser
    {
        /// <summary>
        /// Должность
        /// </summary>
        public List<string>? Position { get; set; } = new();

        /// <summary>
        /// Разрешения на совершения операций
        /// </summary>
        public List<Permission> Permissions { get; set; } = new();
    }
}
