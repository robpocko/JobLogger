using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("CodeBranch")]
    public class CodeBranch : EFJobLogger
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide the name of the code branch")]
        [MaxLength(50, ErrorMessage = "The code branch name cannot be longer than 50 characters")]
        [Display(Name = "Code Branch")]
        public string Name { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public virtual ICollection<CheckIn> BranchCheckIns { get; set; }
    }
}
