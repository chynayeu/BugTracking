using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracking.DAL.Entities
{
    [Table("Comment")]
    public partial class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string text { get; set; }

        public DateTime date { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual User User { get; set; }
    }
}
