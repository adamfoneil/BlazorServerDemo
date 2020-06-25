using Dapper.QX;
using Dapper.QX.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class FolderSelect : Query<KeyValuePair<int, string>>, ITestableQuery
    {
        public FolderSelect() : base(
            $@"WITH [tree] AS ({MyFolderTree.RecursiveQuery}) 
            SELECT [t].[ID] AS [Key], [t].[FullPath] AS [Value]
            FROM [tree] [t]
            WHERE [t].[Id] > 0
            ORDER BY [FullPath]")
        {
        }

        public int WorkspaceId { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new FolderSelect() { WorkspaceId = -1 };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection)
        {
            return TestExecuteHelper(connection);
        }
    }
}
