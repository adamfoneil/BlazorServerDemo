using AO.Models;
using Models.Conventions;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// provides a place to for business to rank value of work items,
    /// and provide business case/requirements
    /// </summary>
    public class WorkItemRequirements : BaseTable, IBody
    {
        [Key]
        [References(typeof(WorkItem))]
        public int WorkItemId { get; set; }

        public int Rank { get; set; }

        public string MarkdownText { get; set; }

        public string HtmlText { get; set; }
    }
}
