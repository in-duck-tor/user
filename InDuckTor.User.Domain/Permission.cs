

namespace InDuckTor.User.Domain
{
    public class Permission
    {
        public required string Key { get; init; }
        public string? Description { get; init; }
        public Role Role { get; init; }
        public virtual List<Employee> Employees { get; set; } = new();
    }
}
