using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Folder : BaseTable
    {
        /// <summary>
        /// when parentId < 0, it's a Workspace.Id * -1
        /// </summary>
        [Key]
        public int ParentId { get; set; }

        [Key]
        [MaxLength(100)]
        public string Name { get; set; }

        [References(typeof(UserProfile))]
        public int? OwnerUserId { get; set; }
    }
}
