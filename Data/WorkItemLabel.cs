using AO.Models;
using Models.Conventions;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class WorkItemLabel : BaseTable
    {
        [Key]
        [References(typeof(WorkItem))]
        public int WorkItemId { get; set; }
        
        [Key]
        [References(typeof(Label))]
        public int LabelId { get; set; }
    }
}
