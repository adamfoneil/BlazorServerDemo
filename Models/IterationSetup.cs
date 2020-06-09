using AO.Models;
using Models.Conventions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class IterationSetup : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// number of working days in a sprint (default 2x Mon -> Fri)
        /// </summary>
        public int WorkingDays { get; set; } = 10;

        /// <summary>
        /// number of off days in a sprint (default 2x Sat + Sun)
        /// </summary>
        public int OffDays { get; set; } = 4;

        public int TotalDays => WorkingDays + OffDays;

        /// <summary>
        /// when do iterations end?
        /// </summary>
        public DayOfWeek EndDay { get; set; } = DayOfWeek.Sunday;
    }
}
