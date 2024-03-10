using FluentResults;

namespace InDuckTor.User.Domain
{
    public static class Errors
    {
        public class Forbidden(string? message = null) : Exception(message);

        public class NotFound(string message) : Exception(message);

        public class Conflict(string message) : Exception(message);

        public static class User
        {
            public class LoginExists(string login) : Errors.Conflict($"Пользователь с логином {login} уже существует в системе");
            public class NotFound(long id) : Errors.NotFound($"Пользователь с Id {id} не существует в системе");
        }

        public static class Permission
        {
            public class NotFound(string key) : Errors.NotFound($"Разрешение с ключом {key} не существует");
        }

        public static class BlackList
        {
            public class BanExists(long userId, DateTime? endDate) : Errors.Conflict($"Пользователь с Id {userId} уже заблокирован" + ((endDate != null) ? $". Дата окончания блокировки - {endDate}" : ""));
            public class NotFound(long userId) : Errors.NotFound($"Пользователь с Id {userId} не заблокирован");
        }
    }
}
