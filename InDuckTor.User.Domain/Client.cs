using System.ComponentModel.DataAnnotations;

namespace InDuckTor.User.Domain
{
    public class Client 
    {
        public long Id { get; set; }

        public virtual User User { get; set; }
    }
}
