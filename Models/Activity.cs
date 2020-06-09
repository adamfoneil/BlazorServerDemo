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
    public class Activity : BaseTable, IValidate
    {
        [Key]
        [References(typeof(Workspace))]
        public int WorkspaceId { get; set; }

        [Key]
        [MaxLength(50)]
        public string Name { get; set; }

        [References(typeof(Activity))]
        public int? NextActivityId { get; set; }

        public ValidateResult Validate()
        {
            return new ValidateResult() { IsValid = true };
        }

        public async Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            var circular = await IsCircularAsync(connection, Id, NextActivityId);
            return (circular) ? 
                new ValidateResult() { IsValid = false, Message = "Activity contains circular reference to next activity." } :
                new ValidateResult() { IsValid = true };
        }

        private static Task<bool> IsCircularAsync(IDbConnection connection, int id, int? nextId)
        {
            if (!nextId.HasValue) return Task.FromResult(false);

            throw new NotImplementedException();
        }
    }
}
