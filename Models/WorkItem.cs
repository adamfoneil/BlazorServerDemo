using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class WorkItem : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        public int Number { get; set; }

        [References(typeof(Folder))]
        public int FolderId { get; set; }

        [MaxLength(255)]
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

        public bool IsClosed { get; set; }
    }
}
