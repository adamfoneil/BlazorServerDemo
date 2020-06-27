using Dapper.QX;
using Dapper.QX.Attributes;
using Dapper.QX.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries
{
    public class OpenWorkItemsResult
    {
        public int Id { get; set; }
        public int WorkspaceId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ActivityId { get; set; }
        public int Iteration { get; set; }
        public int FolderId { get; set; }
        public int? ParentId { get; set; }
        public int? CloseReasonId { get; set; }
        public string FolderPath { get; set; }
        public string CurrentActivity { get; set; }
        public string AssignedTo { get; set; }
    }

    public class OpenWorkItems : Query<OpenWorkItemsResult>, ITestableQuery
    {
        public OpenWorkItems(int rootId) : base(
            $@"WITH [tree] AS (
                {MyFolderTree.GetRecursiveQuery(rootId)}
            ) SELECT
                [wi].*,
                [a].[Name] AS [CurrentActivity],
                COALESCE([au].[DisplayName], [au].[UserName]) AS [AssignedTo],
                [t].[FullPath] AS [FolderPath]
            FROM
                [dbo].[WorkItem] [wi]
                LEFT JOIN [dbo].[Activity] [a] ON [wi].[ActivityId]=[a].[Id]
                LEFT JOIN [dbo].[WorkItemActivity] [wia] ON 
                    [wi].[Id]=[wia].[WorkItemId] AND
                    [wi].[ActivityId]=[wia].[ActivityId]        
                LEFT JOIN [dbo].[AspNetUsers] [au] ON [wia].[UserId]=[au].[UserId]
                INNER JOIN [tree] [t] ON [wi].[FolderId]=[t].[Id]
            WHERE
                [wi].[WorkspaceId]=@workspaceId AND
                [wi].[CloseReasonId] IS NULL 
                {{andWhere}}
            ORDER BY
                [wi].[Number] {{offset}}")
        {
            RootId = rootId;
        }
        
        public int WorkspaceId { get; set; }
        public int RootId { get; private set; }

        [Phrase("Title")]
        public string Text { get; set; }

        [Offset(30)]
        public int? Page { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new OpenWorkItems(-1);
            yield return new OpenWorkItems(1);
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection) => TestExecuteHelper(connection);
    }
}
