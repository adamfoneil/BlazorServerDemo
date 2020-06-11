using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

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
    }
}
