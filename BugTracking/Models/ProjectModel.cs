using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTracking.Models
{
    /// <summary>
    /// Модель проекта для контроллера
    /// </summary>
    public class ProjectModel
    {
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        public int Id { get; set; }

        public  List<UserModel> Users { get; set; }

        public  List<TicketModel> Tickets { get; set; }
    }
}
