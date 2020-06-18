using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// lets you set an icon and description for levels of a folder hierarchy.
    /// For example, this lets you describe a Team/App/Project hierarchy,
    /// or a Team/Client/Project hierarchy that Aerie had at one time
    /// </summary>
    public class FolderLevel : BaseTable
    {
        /// <summary>
        /// to what folder does this level relate to?
        /// For example, we may need to distinguish Dev vs Ops.
        /// positive numbers are folder Ids, negative are workspaceIds
        /// </summary>
        [Key]
        public int FolderOrWorkspaceId { get; set; }

        [Key]
        public int Level { get; set; }

        /// <summary>
        /// what are folders at this depth collectively referred to?        
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Fontawesome icon
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }
    }
}
