using Dapper.QX;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class LabelSelect : Query<KeyValuePair<int, string>>
    {
        public LabelSelect() : base(
            @"SELECT [Id] AS [Key], [Name] AS [Value]
            FROM [dbo].[Label] [l]
            WHERE [WorkspaceId]=@workspaceId AND [IsActive]=1
            ORDER BY [Name]")
        {
        }

        public int WorkspaceId { get; set; }
    }
}
