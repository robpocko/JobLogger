using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("TaskComment")]
    public class TaskComment : EFJobLogger
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a comment")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public long TaskID { get; set; }
    }

}