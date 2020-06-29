using AO.Models;
using AO.Models.Enums;
using AO.Models.Interfaces;
using Dapper;
using Dapper.CX.SqlServer.Extensions.Int;
using Models.Queries;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public partial class WorkItem : ITrigger, IGetRelated, IValidate
    {
        public IEnumerable<int> SelectedLabels { get; set; }

        public async Task GetRelatedAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            SelectedLabels = await new WorkItemLabels() { WorkItemId = Id }.ExecuteAsync(connection, txn);
        }

        public async Task RowDeletedAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            await Task.CompletedTask;
        }

        public async Task RowSavedAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null)
        {
            if (SelectedLabels == null) return;

            await new RebuildWorkItemLabels()
            {
                WorkItemId = Id,
                LabelIds = SelectedLabels.ToArray()
            }.ExecuteAsync(connection, txn);
        }

        public ValidateResult Validate()
        {
            return new ValidateResult() { IsValid = true };
        }

        public async Task<ValidateResult> ValidateAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            if (Number == 0)
            {
                var ws = await connection.GetAsync<Workspace>(WorkspaceId, txn);
                Number = ws.NextWorkItemNumber;
                await connection.ExecuteAsync("UPDATE [dbo].[Workspace] SET [NextWorkItemNumber]=[NextWorkItemNumber]+1 WHERE [Id]=@id", new { id = ws.Id }, txn);
            }

            return new ValidateResult() { IsValid = true };
        }
    }
}
