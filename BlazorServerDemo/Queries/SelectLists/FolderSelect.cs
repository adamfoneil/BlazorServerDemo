using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class FolderSelect : TestableQuery<KeyValuePair<int, string>>
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

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new FolderSelect(-1);
            yield return new FolderSelect(1);
        }
    }
}
