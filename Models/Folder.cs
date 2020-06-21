using AO.Models;
using AO.Models.Enums;
using AO.Models.Interfaces;
using Dapper;
using Models.Conventions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;

namespace Models
{
    /// <summary>
    /// a container of work items or other folders,
    /// which define logical groupings of work items.
    /// Folders consolidate Teams, Applications, and Projects from Gin8, affording a lot of flexibility
    /// in how content is organized.
    /// </summary>
    public class Folder : BaseTable, ITrigger
    {
        /// <summary>
        /// when parentId < 0, it's a Workspace.Id * -1
        /// </summary>
        [Key]
        public int ParentId { get; set; }

        [Key]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// who is allowed to modify this and child folders? null = anyone
        /// </summary>
        [References(typeof(UserProfile))]
        public int? OwnerUserId { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// set during a delete operation to capture child folders that were also deleted
        /// </summary>
        public IEnumerable<int> ChildFolderIds { get; private set; }

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
