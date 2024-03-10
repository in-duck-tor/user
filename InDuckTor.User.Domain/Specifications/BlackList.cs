using System.Linq.Expressions;
namespace InDuckTor.User.Domain.Specifications
{
    public static partial class Specifications
    {
        public static class BlackList
        {
            public static Func<Domain.BlackList, Boolean> ActiveBanAsFunc(long userId)
            {
                return b => b.UserId == userId && (b.EndAt == null ||
                  DateTime.Compare((DateTime)b.EndAt, DateTime.UtcNow) > 0);
            }

            public static Expression<Func<Domain.BlackList, Boolean>> ActiveBan(long userId)
            {
                return b => b.UserId == userId && (b.EndAt == null ||
                  DateTime.Compare((DateTime)b.EndAt, DateTime.UtcNow) > 0);
            }
        }
    }
}
