using AO.Models;
using Models.Conventions;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// defines someone's activity on an item
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
        /// Reasons this work is not on time:
        /// 0 = none (no issues, I should be able to do this),
        /// 1 = underestimated (I'm overbooked on other things, or this is bigger than I thought),
        /// 2 = stumped (I'm not sure how to do this, I need to learn something),
        /// 4 = confused (requirements are problematic, incomplete, contradictory, infeasible)
        /// 8 = blocked (I'm waiting on someone, another group or process)
        /// </summary>
        public int Impediments { get; set; }

        /// <summary>
        /// will I be able to release on time?
        /// </summary>
        public bool? IsOnTime { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal? EstimateDays { get; set; }

        public string MarkdownText { get; set; }

        public string HtmlText { get; set; }
    }
}
