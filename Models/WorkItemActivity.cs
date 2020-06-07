using AO.Models;
using Models.Conventions;
using Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// defines someone's activity on an item, including a difficulty score.
    /// This serves as an estimate, description of scope, or admission of confusion
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
        /// 1 = technical (requirements make sense, but I'm not sure how to do this),
        /// 2 = conceptual (I have a problem with the requirements)
        /// </summary>
        public int Impediments { get; set; }

        public string MarkdownText { get; set; }

        public string HtmlText { get; set; }
    }
}
