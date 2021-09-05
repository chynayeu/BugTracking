using BugTracking.DAL.Entities;
using BugTracking.Models;
using System.Collections.Generic;
using static BugTracking.Services.Util.TicketUtil;

namespace BugTracking.Services.Impl.Converters
{
    public class DBTicketModelConverter : IConverter<Ticket, TicketModel>
    {
        public Ticket Convert(TicketModel source)
        {
            Ticket ticket = new Ticket();

            ticket.Id = source.Id;
            ticket.description = source.Description;
            ticket.status = (int)source.Status;
            ticket.priority = (int)source.Priority;
            ticket.type = (int)source.Type;
            ticket.name = source.Name;
            ticket.assigned = source.Assigned.Id;
            ticket.created = source.Created.Id;

            ticket.Comments = new List<Comment>();

            return ticket;
        }

        public TicketModel Convert(Ticket source)
        {
            TicketModel ticket = new TicketModel();

            ticket.Id = source.Id;
            ticket.Description = source.description;
            ticket.Status = (TicketStatus)source.status;
            ticket.Priority = (TicketPriority)source.priority;
            ticket.Type = (TicketType)source.type;
            ticket.Name = source.name;

            ticket.Comments = new List<CommentModel>();

            return ticket;
        }
    }
}
