using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class LabelSelect : TestableQuery<KeyValuePair<int, string>>
    {
        public LabelSelect() : base(
            @"SELECT [Id] AS [Key], [Name] AS [Value]
            FROM [dbo].[Label] [l]
            WHERE [WorkspaceId]=@workspaceId AND [IsActive]=1
            ORDER BY [Name]")
        {
        }

        public int WorkspaceId { get; set; }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new LabelSelect() { WorkspaceId = 223 };
        }
    }
}
