using Dapper.QX;
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
        public OpenWorkItems() : base(
            $@"WITH [tree] AS (
                SELECT
                    [f].[Id], [f].[Name], 0 AS [Level], [f].[ParentId], CAST([f].[Name] AS varchar(255)) AS [FullPath]
                FROM
                    [dbo].[Folder] [f]
                WHERE
                    [f].[Id]=@rootFolderId

                UNION ALL

                SELECT
                    [f].[Id], [f].[Name], [t].[Level]+1 AS [Level], [f].[ParentId], CAST(CONCAT([t].[FullPath], ' / ', [f].[Name]) as varchar(255)) AS [FullPath]
                FROM 
                    [dbo].[Folder] [f]                    
                    INNER JOIN [tree] [t] ON [f].[ParentId]=[t].[Id]
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
                [wi].[CloseReasonId] IS NULL")
        {
        }
        
        public int WorkspaceId { get; set; }
        public int RootFolderId { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new OpenWorkItems() { WorkspaceId = -1 };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection) => TestExecuteHelper(connection);
    }
}
