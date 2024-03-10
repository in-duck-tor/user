using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace InDuckTor.User.Domain
{
    public class User
    {
        public long Id { get; init; }

        public required string Login { get; init; }

        public required AccountType AccountType { get; init; }
    }

    public enum AccountType
    {
        [EnumMember(Value = "employee")] Employee,

        [EnumMember(Value = "client")] Client,
    }
}
