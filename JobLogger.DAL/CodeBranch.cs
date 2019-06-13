using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("CodeBranch")]
    public class CodeBranch : EFBase
    {
        [Key]
        [Required(ErrorMessage = "ID for record has not been provided")]
        new public long ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide the name of the code branch")]
        [MaxLength(50, ErrorMessage = "The code branch name cannot be longer than 50 characters")]
        [Display(Name = "Code Branch")]
        public string Name { get; set; }

        public virtual ICollection<CheckIn> BranchCheckIns { get; set; }
    }
}
