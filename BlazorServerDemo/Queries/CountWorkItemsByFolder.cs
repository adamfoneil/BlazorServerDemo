using Dapper.QX;
using Dapper.QX.Abstract;
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

    public class CountWorkItemsByFolder : TestableQuery<CountWorkItemsByFolderResult>
    {
        public CountWorkItemsByFolder(int rootId) : base(
            $@"WITH [tree] AS (
                {MyFolderTree.GetRecursiveQuery(rootId)}
            ) SELECT 
                [f].[Id] AS [FolderId],
                (SELECT COUNT(1) FROM [dbo].[WorkItem] WHERE [FolderId]=[f].[Id] AND [CloseReasonId] IS NULL) AS [OpenCount],
                (SELECT COUNT(1) FROM [dbo].[WorkItem] WHERE [FolderId]=[f].[Id] AND [CloseReasonId] IS NOT NULL) AS [ClosedCount]
             FROM
                [dbo].[Folder] [f]                
                INNER JOIN [tree] [t] ON [f].[Id]=[t].[Id]")
        {
            RootId = rootId;
        }

        public int RootId { get; private set; }

        public async Task<Dictionary<int, CountWorkItemsByFolderResult>> ExecuteDictionaryAsync(IDbConnection connection)
        {
            return (await ExecuteAsync(connection)).ToDictionary(row => row.FolderId);
        }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new CountWorkItemsByFolder(-1);
            yield return new CountWorkItemsByFolder(1);
        }
    }
}
