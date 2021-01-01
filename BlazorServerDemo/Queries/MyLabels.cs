using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries
{
    public class MyLabels : TestableQuery<Label>
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

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new MyLabels() { WorkspaceId = 1, IsActive = true };
        }
    }
}
