using System.ComponentModel;

namespace BugTracking.Services.Util
{
    public static class TicketUtil
    {
        public enum TicketType
        {
            [Description("Баг")]
            bug,
            [Description("Сторя")]
            story,
            [Description("Квери")]
            query
        }

        /// <summary>
        /// Перечисление приоритетов тикета
        /// </summary>
        public enum TicketPriority
        {
            [Description("Наивысший")]
            p1,
            [Description("Высокий")]
            p2,
            [Description("Средний")]
            p3,
            [Description("Низкий")]
            p4
        }
        /// <summary>
        /// Перечисление статусов тикета
        /// </summary>
        public enum TicketStatus
        {
            [Description("Открытый")]
            open,
            [Description("В процессе")]
            inProgress,
            [Description("Готов для тестирования")]
            readyForTest,
            [Description("В тестировании")]
            inTest,
            [Description("Закрыт")]
            closed
        }
    }
}
