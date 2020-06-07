using Models.Conventions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Workspace : BaseTable
    {
        [Key]
        [MaxLength(100)]
        public string Name { get; set; }

        public int NextWorkItemNumber { get; set; } = 1000;

        /// <summary>
        /// number of days in a sprint
        /// </summary>
        public int IterationDays { get; set; } = 14;

        /// <summary>
        /// when do iterations end?
        /// </summary>
        public DayOfWeek IterationEndDay { get; set; } = DayOfWeek.Sunday;
    }
}
