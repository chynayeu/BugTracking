using System;

namespace BugTracking.Models
{
    /// <summary>
    /// Модель комментария для контроллера
    /// </summary>
    public class CommentModel
    {
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public TicketModel Ticket { get; set; }
        public UserModel User { get; set; }
    }
}
