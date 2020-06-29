using AO.Models;
using Models.Conventions;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public partial class Label : BaseTable, ILabel
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
    }
}
