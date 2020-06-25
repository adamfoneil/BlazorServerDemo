using AO.Models;
using AO.Models.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Models
{
    public partial class IterationSchedule : IValidate
    {
        public ValidateResult Validate()
        {
            return ((TotalDays % 7) == 0) ?
                new ValidateResult() { IsValid = true } :
                new ValidateResult() { IsValid = false, Message = "Total days must be a multiple of 7." };
        }

        public Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            return Task.FromResult(new ValidateResult() { IsValid = true });
        }

        //public DateTime GetCurrent() => DateTime.Today.DayOfWeek
    }
}
