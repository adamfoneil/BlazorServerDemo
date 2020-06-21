using AO.Models;
using Models.Conventions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// a container of work items or other folders,
    /// which define logical groupings of work items.
    /// Folders consolidate Teams, Applications, and Projects from Gin8, affording a lot of flexibility
    /// in how content is organized.
    /// </summary>
    public partial class Folder : BaseTable
    {
        /// <summary>
        /// when parentId < 0, it's a Workspace.Id * -1
        /// </summary>
        [Key]
        public int ParentId { get; set; }

        [Key]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// who is allowed to modify this and child folders? null = anyone
        /// </summary>
        [References(typeof(UserProfile))]
        public int? OwnerUserId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// set during a delete operation to capture child folders that were also deleted
        /// </summary>
        public IEnumerable<int> ChildFolderIds { get; private set; }        
    }
}
