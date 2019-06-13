using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("CheckIn")]
    public class CheckIn : EFBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a comment for the check-in")]
        [MaxLength(1000, ErrorMessage = "The check-in comment cannot be longer than 1000 characters")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "You must provide the date for the check-in")]
        [Display(Name = "Check-in Time")]
        public DateTime CheckInTime { get; set; }

        public long? TaskLogID { get; set; }
        public TaskLog TaskLog { get; set; }

        public long CodeBranchID { get; set; }
        public CodeBranch CodeBranch { get; set; }

        public virtual ICollection<TaskCheckIn> TaskCheckIns { get; set; }
    }
}
