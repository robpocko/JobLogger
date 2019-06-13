using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobLogger.DAL.Common;

namespace JobLogger.DAL
{
    [Table("Task")]
    public class Task : EFBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide the title for the task")]
        [MaxLength(255, ErrorMessage = "The task title cannot be longer than 255 characters")]
        [Display(Name = "Task Title")]
        public string Title { get; set; }

        [Display(Name = "Task Type")]
        public TaskType TaskType { get; set; }

        [Required(ErrorMessage = "You must provide a value for IsActive")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public long? RequirementID { get; set; }

        public Requirement Requirement { get; set; }

        public virtual ICollection<TaskLog> Logs { get; set; }

        public virtual ICollection<TaskCheckIn> CheckIns { get; set; }

        public virtual ICollection<TaskComment> Comments { get; set; }
    }
}
