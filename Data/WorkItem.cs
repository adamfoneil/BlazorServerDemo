using AO.Models;
using AO.Models.Enums;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public partial class WorkItem : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        [SaveAction(SaveAction.Insert)]
        public int WorkspaceId { get; set; }

        [Key]
        [SaveAction(SaveAction.Insert)]
        public int Number { get; set; }

        [References(typeof(Folder))]
        public int FolderId { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// -1 = backlog, 0 = current iteration, 1 = next iteration, etc
        /// </summary>
        public int Iteration { get; set; }

        /// <summary>
        /// current activity, if any
        /// </summary>
        [References(typeof(Activity))]
        public int? ActivityId { get; set; }

        [References(typeof(WorkItem))]
        public int? ParentId { get; set; }

        [References(typeof(CloseReason))]
        public int? CloseReasonId { get; set; }
    }
}
