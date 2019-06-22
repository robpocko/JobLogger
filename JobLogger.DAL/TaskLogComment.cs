using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("TaskLogComment")]
    public class TaskLogComment : EFBase
    {
        [Key]
        [Required(ErrorMessage = "ID for record has not been provided")]
        new public long ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a comment")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public long TaskLogID { get; set; }
    }
}
