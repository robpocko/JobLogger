using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobLogger.DAL.Common;

namespace JobLogger.DAL
{
    [Table("TimeLine")]
    public class TimeLine : EFJobLogger
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide the title for the time line")]
        [MaxLength(255, ErrorMessage = "The time line title cannot be longer than 255 characters")]
        [Display(Name = "Time Line Title")]
        public string Title { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<TaskLog> TaskLogs { get; set; }
    }
}
