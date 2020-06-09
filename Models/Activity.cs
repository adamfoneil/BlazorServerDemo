using AO.Models;
using AO.Models.Interfaces;
using Models.Conventions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// defines an activity such as Design, Develop, Test, Deploy
    /// </summary>
    public class Activity : BaseTable
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
