using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracking.Services
{
    interface ICommentService<C,T>
    {
        /// <summary>
        /// Метод для добавление коментария к тикету
        /// </summary>
        /// <param name="text">текст комментария</param>
        /// <param name="ticket">тикет для которого коментарий добавляется</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public bool CreateComment(string text, T ticket);

        /// <summary>
        /// Метод для получения комментариев для тикета
        /// </summary>
        /// <param name="ticket">тикет для поиска коментариев</param>
        /// <returns>список комментариев для тикета</returns>
        public List<C> ReadTicketComments(T ticket);
    }
}
