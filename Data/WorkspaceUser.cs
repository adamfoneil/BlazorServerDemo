using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class WorkspaceUser : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        [References(typeof(UserProfile))]
        public int UserId { get; set; }

        /// <summary>
        /// This is a join request (or invite)
        /// </summary>
        public bool IsRequest { get; set; }

        /// <summary>
        /// User is allowed into the workspace (join request accepted)
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
