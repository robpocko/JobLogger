using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobLogger.DAL.Common;

namespace JobLogger.DAL
{
    [Table("Feature")]
    public class Feature : EFTFS
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide the title for the feature")]
        [MaxLength(255, ErrorMessage = "The feature title cannot be longer than 255 characters")]
        [Display(Name = "Feature Title")]
        public string Title { get; set; }

        public RequirementStatus Status { get; set; }

        public virtual ICollection<Requirement> Requirements { get; set; }
    }
}
