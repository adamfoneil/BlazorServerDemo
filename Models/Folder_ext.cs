using AO.Models.Enums;
using AO.Models.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Models
{
    public partial class Folder : ITrigger
    {
        public async Task RowDeletedAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            var childFolders = new List<int>();
            await deleteChildren(Id, childFolders);
            ChildFolderIds = childFolders;

            async Task deleteChildren(int parentId, List<int> deleted)
            {
                var children = await connection.QueryAsync<int>("SELECT [Id] FROM [dbo].[Folder] WHERE [ParentId]=@id", new { id = parentId }, txn);
                foreach (var childId in children)
                {
                    await connection.ExecuteAsync("DELETE [dbo].[Folder] WHERE [Id]=@id", new { id = childId }, txn);
                    deleted.Add(childId);
                    await deleteChildren(childId, deleted);
                }
            }
        }

        public async Task RowSavedAsync(IDbConnection connection, SaveAction saveAction, IDbTransaction txn = null)
        {
            await Task.CompletedTask;
        }
    }
}
