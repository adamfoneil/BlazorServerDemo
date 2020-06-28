using Dapper.QX;
using Dapper.QX.Abstract;
using Dapper.QX.Interfaces;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries
{
    public class OpenWorkItemLabelCountsResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public int Count { get; set; }
    }

    public class OpenWorkItemLabelCounts : TestableQuery<OpenWorkItemLabelCountsResult>
    {
        public OpenWorkItemLabelCounts(int rootId) : base(
            $@"WITH [tree] AS (
                {MyFolderTree.GetRecursiveQuery(rootId)}
            ), [source] AS (
                SELECT
                    [lbl].[Id],
                    [lbl].[Name],
                    [lbl].[BackColor],
                    [lbl].[TextColor],
                    (
                        SELECT 
                            COUNT(1) 
                        FROM 
                            [dbo].[WorkItemLabel] [wil] 
                            INNER JOIN [dbo].[WorkItem] [wi] ON [wil].[WorkItemId]=[wi].[Id] 
                            INNER JOIN [tree] [t] ON [wi].[FolderId]=[t].[Id]
                        WHERE                     
                            [wil].[LabelId]=[lbl].[Id] AND 
                            [wi].[WorkspaceId]=@workspaceId AND
                            [wi].[CloseReasonId] IS NULL
                    ) AS [Count]
                FROM
                    [dbo].[Label] [lbl]
                WHERE
                    [lbl].[WorkspaceId]=@workspaceId
            ) SELECT 
                [source].*
            FROM 
                [source]
            ORDER BY
                [Count] DESC")
        {
            RootId = rootId;
        }

        public int RootId { get; private set; }
        public int WorkspaceId { get; set; }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new OpenWorkItemLabelCounts(-1) { WorkspaceId = 3 };
            yield return new OpenWorkItemLabelCounts(1) { WorkspaceId = 3 };
        }
    }
}
