using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("TaskComment")]
    public class TaskComment : EFBase
    {
        [Key]
        [Required(ErrorMessage = "ID for record has not been provided")]
        new public long ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a comment")]
        [MaxLength(1000, ErrorMessage = "The comment cannot be longer than 1000 characters")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public long TaskID { get; set; }
    }

}