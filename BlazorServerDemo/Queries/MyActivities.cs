using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries
{
    public class MyActivities : TestableQuery<Activity>
    {
        public MyActivities() : base(
            @"SELECT 
                [a].*,
                (SELECT COUNT(1) FROM [dbo].[WorkItem] [wi] INNER JOIN [dbo].[WorkItemActivity] [wia] ON [wi].[Id]=[wia].[WorkItemId] WHERE [wia].[ActivityId]=[a].[Id] AND [wi].[CloseReasonId] IS NULL) AS [OpenWorkItems]
            FROM 
                [dbo].[Activity] [a]
            WHERE
                [a].[WorkspaceId]=@workspaceId AND
                [a].[IsActive]=@isActive
            ORDER BY
                [a].[Order]")
        {
        }

        public int WorkspaceId { get; set; }
        public bool IsActive { get; set; }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new MyActivities() { WorkspaceId = 1, IsActive = true };
        }
    }
}
