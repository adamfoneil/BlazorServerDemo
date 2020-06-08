using AO.Models;
using Models.Conventions;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// defines someone's activity on an item, including a difficulty score.
    /// This serves as an estimate, description of scope, admission of confusion, or place for status update
    /// </summary>
    public class WorkItemActivity : BaseTable, IBody
    {
        [Key]
        [References(typeof(WorkItem))]
        public int WorkItemId { get; set; }

        [Key]
        [References(typeof(Activity))]
        public int ActivityId { get; set; }

        [References(typeof(UserProfile))]
        public int? UserId { get; set; }

        /// <summary>
        /// Flag values:
        /// 0 = none (no issues, I should be able to do this),
        /// 1 = technical (I'm not sure how to do this, it's more complex than I thought),
        /// 2 = conceptual (requirements are problematic, contradictory, infeasible)
        /// </summary>
        public int Impediments { get; set; }

        /// <summary>
        /// will I be able to release on time?
        /// </summary>
        public bool? IsOnTime { get; set; }

        public int? EstimateHours { get; set; }

        public string MarkdownText { get; set; }

        public string HtmlText { get; set; }
    }
}
