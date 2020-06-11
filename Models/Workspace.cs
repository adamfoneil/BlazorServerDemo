using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Workspace : BaseTable
    {
        [Key]
        [MaxLength(100)]
        public string Name { get; set; }

        public int NextWorkItemNumber { get; set; } = 1000;

        [References(typeof(UserProfile))]
        public int? OwnerUserId { get; set; }

        [References(typeof(IterationSetup))]
        public int? IterationSetupId { get; set; }
    }
}
