using Dapper.QX;
using Dapper.QX.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class FolderSelect : Query<KeyValuePair<int, string>>, ITestableQuery
    {
        public FolderSelect(int rootId) : base(
            $@"WITH [tree] AS ({MyFolderTree.GetRecursiveQuery(rootId)}) 
            SELECT [t].[ID] AS [Key], [t].[FullPath] AS [Value]
            FROM [tree] [t]
            WHERE [t].[Id] > 0
            ORDER BY [FullPath]")
        {
            RootId = rootId;
        }

        public int RootId { get; private set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new FolderSelect(-1);
            yield return new FolderSelect(1);
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection)
        {
            return TestExecuteHelper(connection);
        }
    }
}
