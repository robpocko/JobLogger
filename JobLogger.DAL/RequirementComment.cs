using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("RequirementComment")]
    public class RequirementComment : EFJobLogger
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide a comment")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public long RequirementID { get; set; }
    }
}
