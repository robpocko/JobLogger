using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobLogger.DAL.Common;

namespace JobLogger.DAL
{
    [Table("Requirement")]
    public class Requirement : EFTFS
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide the title for the requirement")]
        [MaxLength(255, ErrorMessage = "The requirement title cannot be longer than 255 characters")]
        [Display(Name = "Requirement Title")]
        public string Title { get; set; }

        public RequirementStatus Status { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public long? FeatureID { get; set; }

        public Feature Feature { get; set; }

        public virtual ICollection<RequirementComment> Comments { get; set; }
    }
}
