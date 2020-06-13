using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Label : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string BackColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string TextColor { get; set; }

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public int OpenWorkItems { get; set; }
    }
}
