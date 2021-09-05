using System.Collections.Generic;

namespace BugTracking.Services
{
    public interface IUserService<U, R>
    {
        /// <summary>
        /// Метод для добавления пользователя по-умолчанию (при регистрации) 
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="email">Имейл</param>
        /// <param name="userRoles">роли пользователя</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool CreateUser(string name, string surname, string email, List<R> userRoles);

        /// <summary>
        /// Получение пользователя по имейлу
        /// </summary>
        /// <param name="userEmail">Имейл пользователя</param>
        /// <returns>Пользователя или null, в случае ошибки</returns>
        public  U ReadUser(string userEmail);

        /// <summary>
        /// Метод для обновления пользователя 
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool UpdateUser(U user);

        /// <summary>
        /// Метод для удаления пользователя
        /// </summary>
        /// <param name="user">Пользователь для удаления</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool DeleteUser(U user);

        /// <summary>
        /// Метод для получения всех пользователей
        /// </summary>
        /// <returns>Список пользователей или null, в случае ошибки</returns>
        public List<U> AllUsers();
    }
}
