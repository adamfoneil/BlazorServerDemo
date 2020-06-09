using AO.Models;
using Models.Conventions;
using Models.Interfaces;

namespace Models
{
    public class HandOff : BaseTable, IBody
    {
        [References(typeof(WorkItem))]
        public int WorkItemId { get; set; }

        [References(typeof(UserProfile))]
        public int FromUserId { get; set; }

        [References(typeof(Activity))]
        public int FromActivityId { get; set; }

        [References(typeof(Activity))]
        public int ToActivityId { get; set; }

        public bool IsForward { get; set; }

        public string MarkdownText { get; set; }

        public string HtmlText { get; set; }
    }
}
