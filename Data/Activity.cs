using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// defines an activity such as Design, Develop, Test, Deploy
    /// </summary>
    public class Activity : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; } = true;

        [NotMapped]
        public int OpenWorkItems { get; set; }
    }
}
