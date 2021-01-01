using Dapper.QX;
using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Models.Queries
{
    public class RebuildWorkItemLabels : TestableQuery<int>
    {
        public RebuildWorkItemLabels() : base(
            @"DELETE [dbo].[WorkItemLabel] WHERE [WorkItemId]=@workItemId;

            INSERT INTO [dbo].[WorkItemLabel] (
                [WorkItemId], [LabelId], [DateCreated], [CreatedBy]
            ) SELECT
                [wi].[Id], [lbl].[Id], COALESCE([wi].[DateModified], [wi].[DateCreated]), COALESCE([wi].[ModifiedBy], [wi].[CreatedBy])
            FROM
                [dbo].[WorkItem] [wi],
                [dbo].[Label] [lbl]
            WHERE
                [wi].[Id]=@workItemId AND
                [lbl].[Id] IN @labelIds")
        {
        }

        public int WorkItemId { get; set; }
        public int[] LabelIds { get; set; }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new RebuildWorkItemLabels() { WorkItemId = -1, LabelIds = new int[] { 1, 2, 3 } };
        }
    }
}
