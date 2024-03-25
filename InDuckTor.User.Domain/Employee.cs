namespace InDuckTor.User.Domain
{
    public class Employee 
    {
        public long Id { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public List<string> Position { get; set; } = new();

        /// <summary>
        /// Ключи разрешения на совершение операций
        /// </summary>
        public List<Permission> Permissions { get; set; } = new();

        public virtual User User {  get; set; }
    }
}
