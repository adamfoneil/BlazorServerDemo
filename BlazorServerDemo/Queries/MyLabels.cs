using Dapper.QX;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries
{
    public class MyLabels : Query<Label>, ITestableQuery
    {
        public MyLabels() : base(
            @"SELECT 
                [lbl].*,
                (SELECT COUNT(1) FROM [dbo].[WorkItem] [wi] INNER JOIN [dbo].[WorkItemLabel] [wil] ON [wi].[Id]=[wil].[WorkItemId] WHERE [LabelId]=[lbl].[Id] AND [wi].[CloseReasonId] IS NULL) AS [OpenWorkItems]
            FROM 
                [dbo].[Label] [lbl]
            WHERE 
                [WorkspaceId]=@workspaceId AND 
                [IsActive]=@isActive 
            ORDER BY
                [Name]")
        {
        }

        public int WorkspaceId { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new MyLabels() { WorkspaceId = 1, IsActive = true };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection)
        {
            return TestExecuteHelper(connection);
        }
    }
}
