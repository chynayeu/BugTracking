using BugTracking.DAL.Data;
using BugTracking.DAL.Entities;
using BugTracking.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracking.Services.Impl
{
    public class DBTicketServiceImpl : ITicketService<TicketModel, ProjectModel, CommentModel>
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IConverter<Ticket, TicketModel> _converter;
        private readonly IConverter<Comment, CommentModel> _commentConverter;

        public DBTicketServiceImpl(ApplicationDbContext context, ILogger<DBTicketServiceImpl> logger,
            IConverter<Ticket, TicketModel> converter, IConverter<Comment, CommentModel> commentConverter)
        {
            _context = context;
            _logger = logger;
            _converter = converter;
            _commentConverter = commentConverter;
        }

        public bool CreatTicket(TicketModel ticket, ProjectModel project)
        {
            try
            {
                Ticket newTicket = _converter.Convert(ticket);
                newTicket.Project = _context.Projects.SingleOrDefault(b => b.Id == project.Id);
                _context.Tickets.Add(newTicket);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка добавления тикета " + e.Message);
                return false;
            }

            return true;
        }

        public bool DeleteTicket(TicketModel ticket)
        {
            try
            {
                if (ticket != null)
                {
                    Ticket tkt = _context.Tickets.SingleOrDefault(t => t.Id == ticket.Id);
                    _context.Tickets.Remove(tkt);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка удаления проекта : " + e.Message);
                return false;
            }
            return true;
        }

        public List<TicketModel> ReadTicketsByProjectId(ProjectModel project)
        {
            List<TicketModel> result = new List<TicketModel>();
            try
            {
                //Explicit load of tickets
                Project projectDB = _context.Projects.FirstOrDefault(p => p.Id == project.Id);
                _context.Entry(projectDB).Collection(p => p.Tickets).Load();
                if (projectDB.Tickets != null)
                {
                    projectDB.Tickets.ToList().ForEach(t => result.Add(_converter.Convert(t)));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка обноления тикета " + e.Message);
                return null;
            }
            return result;
        }

        public bool UpdateTicket(TicketModel ticket)
        {
            try
            {
                Ticket ticketToUpdate = _context.Tickets.SingleOrDefault(b => b.Id == ticket.Id);
                _context.Entry(ticketToUpdate).Reference(t => t.Project).Load();
                _context.Entry(ticketToUpdate).Collection(t => t.Comments).Load();
                copyTicket(ticketToUpdate, _converter.Convert(ticket));
                if (ticketToUpdate != null)
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка обноления тикета " + e.Message);
                return false;
            }

            return true;
        }

        public bool AddComment(CommentModel comment, TicketModel ticket)
        {
            try
            {
                Comment addComment = new Comment();
                addComment.text = comment.text;
                addComment.User = _context.Users.SingleOrDefault(u => u.Id == comment.User.Id);
                addComment.Ticket = _context.Tickets.SingleOrDefault(t => t.Id == ticket.Id);
                addComment.date = DateTime.Now;
                _context.Comments.Add(addComment);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка добавления кометария " + e.Message);
                return false;
            }

            return true;
        }

        public List<CommentModel> getTicketComments(TicketModel ticket)
        {
            List<CommentModel> result = new List<CommentModel>();

            try
            {
                Ticket ticketFromdb = _context.Tickets.SingleOrDefault(t => t.Id == ticket.Id);
                _context.Entry(ticketFromdb).Collection(t => t.Comments).Load();
                if (ticketFromdb.Comments != null)
                {
                    ticketFromdb.Comments.ToList().ForEach(c => {
                        _context.Entry(c).Reference(u => u.User).Load();
                        result.Add(_commentConverter.Convert(c));
                        });
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Ошибка получения коментвриев для тикета " + e.Message);
                return null;
            }

            return result;
        }

        /// <summary>
        /// Метод для копирования тикета
        /// </summary>
        /// <param name="ticketTo">тикет в который копируют</param>
        /// <param name="ticketFrom">тикет из которого копируют</param>
        private static void copyTicket(Ticket ticketTo, Ticket ticketFrom)
        {
            ticketTo.name = ticketFrom.name;
            ticketTo.priority = ticketFrom.priority;
            ticketTo.status = ticketFrom.status;
            ticketTo.type = ticketFrom.type;
            ticketTo.assigned = ticketFrom.assigned;
            ticketTo.created = ticketFrom.created;
            ticketTo.description = ticketFrom.description;
        }
    }
}
