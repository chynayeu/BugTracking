using static BugTracking.Services.Util.TicketUtil;
using System.Collections.Generic;

namespace BugTracking.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        public TicketType Type { get; set; }

        public TicketPriority Priority { get; set; }

        public TicketStatus Status { get; set; }

        public string Description { get; set; }

        public UserModel Assigned { get; set; }

        public UserModel Created { get; set; }

        public string Name { get; set; }

        public virtual List<CommentModel> Comments { get; set; }

    }
}
