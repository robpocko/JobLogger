using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("TaskLog")]
    public class TaskLog : EFBase
    {
        [Key]
        [Required(ErrorMessage = "ID for record has not been provided")]
        new public long ID { get; set; }

        [Required(ErrorMessage = "You must provide the date for the log entry")]
        [Display(Name = "Log Date")]
        [Column(TypeName = "Date")]
        public DateTime LogDate { get; set; }

        [Required(ErrorMessage = "You must provide the starting time for the log entry")]
        [Display(Name = "Starting Time")]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        public TimeSpan? EndTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a description for the log entry")]
        [MaxLength(255, ErrorMessage = "The log description cannot be longer than 255 characters")]
        [Display(Name = "Log Description")]
        public string Description { get; set; }

        public long? TaskID {get; set; }
        public Task Task { get; set; }

        public virtual ICollection<TaskLogComment> Comments { get; set; }

        public virtual ICollection<CheckIn> CheckIns { get; set; }
    }
}
