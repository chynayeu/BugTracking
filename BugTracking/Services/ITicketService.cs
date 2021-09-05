using System.Collections.Generic;

namespace BugTracking.Services.Impl
{
    interface ITicketService<T, P, C>
    {
        /// <summary>
        /// Метод для создания тикета
        /// </summary>
        /// <param name="ticket">тикет для добавления</param>
        /// <param name="project">проект в который тикет добавляется</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool CreatTicket(T ticket, P project);

        /// <summary>
        /// Метод для получения тикетов для проекта
        /// </summary>
        /// <param name="project">проект для поиска</param>
        /// <returns>Список тикетов для проекта</returns>
        public List<T> ReadTicketsByProjectId(P project);

        /// <summary>
        /// Метод для обновления тикета
        /// </summary>
        /// <param name="ticket">тикет для обновления</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool UpdateTicket(T ticket);

        /// <summary>
        /// Метод для удаления тикета
        /// </summary>
        /// <param name="ticket">тикет для удаления</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool DeleteTicket(T ticket);

        /// <summary>
        /// Метод для добавление коментария к тикету
        /// </summary>
        /// <param name="comment">комментарий для добавления</param>
        /// <param name="ticket">к какому тикету добавить</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool AddComment(C comment, T ticket);

        /// <summary>
        /// Метод для получения комментариев для тикета
        /// </summary>
        /// <param name="ticket">тикет для поиска коментариев</param>
        /// <returns>список комментариев для тикета</returns>
        public List<C> getTicketComments(T ticket);
    }
}
