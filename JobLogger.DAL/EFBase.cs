﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    public abstract class EFBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "ID for record has not been provided")]
        public long ID { get; set; }

        [NotMapped()]
        public bool IsNew { get; set; } = false;

        [NotMapped()]
        public List<ValidationResult> ValidationResults { get; set; }

        public virtual bool IsValid()
        {
            ValidationResults = new List<ValidationResult>();
            var vc = new ValidationContext(this, null, null);

            return Validator.TryValidateObject(this, vc, ValidationResults, true);
        }
    }
}
