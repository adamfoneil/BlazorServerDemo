using Dapper.QX;
using Dapper.QX.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerDemo.Queries
{
    public class CountWorkItemsByFolderResult
    {
        public int FolderId { get; set; }
        public int OpenCount { get; set; }
        public int ClosedCount { get; set; }
    }

    public class CountWorkItemsByFolder : Query<CountWorkItemsByFolderResult>, ITestableQuery
    {
        public CountWorkItemsByFolder() : base(
            $@"WITH [tree] AS (
                {MyFolderTree.RecursiveQuery}
            ) SELECT 
                [f].[Id] AS [FolderId],
                (SELECT COUNT(1) FROM [dbo].[WorkItem] WHERE [FolderId]=[f].[Id] AND [CloseReasonId] IS NULL) AS [OpenCount],
                (SELECT COUNT(1) FROM [dbo].[WorkItem] WHERE [FolderId]=[f].[Id] AND [CloseReasonId] IS NOT NULL) AS [ClosedCount]
             FROM
                [dbo].[Folder] [f]                
                INNER JOIN [tree] [t] ON [f].[Id]=[t].[Id]")
        {
        }

        public int WorkspaceId { get; set; }

        public async Task<Dictionary<int, CountWorkItemsByFolderResult>> ExecuteDictionaryAsync(IDbConnection connection)
        {
            return (await ExecuteAsync(connection)).ToDictionary(row => row.FolderId);
        }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new CountWorkItemsByFolder() { WorkspaceId = -1 };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection) => TestExecuteHelper(connection);
    }
}
