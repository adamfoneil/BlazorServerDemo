using Dapper.QX;
using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries
{
    public class OpenWorkItemLabelsResult
    {
        public int WorkItemId { get; set; }
        public int LabelId { get; set; }
        public string Name { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }        
    }

    public class OpenWorkItemLabels : TestableQuery<OpenWorkItemLabelsResult>
    {
        public OpenWorkItemLabels() : base(
            @"SELECT
                [wil].[WorkItemId],
                [wil].[LabelId],
                [lbl].[Name],
                [lbl].[BackColor],
                [lbl].[TextColor]
            FROM
                [dbo].[WorkItem] [wi]
                INNER JOIN [dbo].[WorkItemLabel] [wil] ON [wi].[Id]=[wil].[WorkItemId]
                INNER JOIN [dbo].[Label] [lbl] ON [wil].[LabelId]=[lbl].[Id]
            WHERE
                [wi].[WorkspaceId]=@workspaceId AND
                [wi].[CloseReasonId] IS NULL")
        {
        }

        public int WorkspaceId { get; set; }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new OpenWorkItemLabels() { WorkspaceId = -1 };
        }
    }
}
