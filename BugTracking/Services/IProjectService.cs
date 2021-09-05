using System.Collections.Generic;

namespace BugTracking.Services
{
    /// <summary>
    /// Сервис для работы с проектами
    /// </summary>
    /// <typeparam name="P">Модель проекта</typeparam>
    /// <typeparam name="U">Модель пользователя</typeparam>
    public interface IProjectService<P,U>
    {
        /// <summary>
        /// Метод добавления проекта
        /// </summary>
        /// <param name="name">название проекта</param>
        /// <param name="description">описание проекта</param>
        /// <returns>true, если добавлен, иначе - false</returns>
        public bool CreateProject(string name, string description);

        /// <summary>
        /// Возвращает все проекты для пользователя
        /// </summary>
        /// <param name="userEmail">имейл пользователя</param>
        /// <returns>список проектов</returns>
        public List<P> ReadUserProjects(string userEmail);

        /// <summary>
        /// Возвращает все проекты для пользователя
        /// </summary>
        /// <returns>список всех проектов</returns>
        public List<P> ReadAllProjects();

        /// <summary>
        /// Метод для обновления проекта
        /// </summary>
        /// <param name="project">обнавлённый проект</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool UpdateProject(P project);

        /// <summary>
        /// Метод для удаления проекта
        /// </summary>
        /// <param name="project">Проект для удаления</param>
        /// <returns>ttrue, если удачно, иначе - false</returns>
        public bool DeleteProject(P project);

        /// <summary>
        /// Метод для получения пользователей для проекта
        /// </summary>
        /// <param name="projectId">Айди проекта</param>
        /// <returns>Список пользователей или null, в случае ошибки</returns>
        public List<U> GetUsersForProject(int projectId);

        /// <summary>
        /// Метод для обновления пользователей для проекта
        /// </summary>
        /// <param name="projectId">Айди проекта</param>
        /// <param name="userEmails">список имейлов пользователей</param>
        /// <returns>Список пользователей или null, в случае ошибки</returns>
        public bool UpdateUsersForProject(int projectId, List<string> userEmails);

    }
}
