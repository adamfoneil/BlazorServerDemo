using AO.Models;
using AO.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Conventions
{
    [Identity(nameof(Id))]
    public abstract class BaseTable
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [SaveAction(SaveAction.Insert)]
        public string CreatedBy { get; set; }

        [SaveAction(SaveAction.Insert)]
        public DateTime DateCreated { get; set; }

        [SaveAction(SaveAction.Update)]
        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        [SaveAction(SaveAction.Update)]
        public DateTime? DateModified { get; set; }
    }
}
