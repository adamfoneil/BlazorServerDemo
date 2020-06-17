using Dapper.QX;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BlazorServerDemo.Queries
{
    public class MyFolderTreeResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int ParentId { get; set; }
    }

    public class FolderStructure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public IEnumerable<FolderStructure> Children { get; set; }

        public static FolderStructure FromFolders(IEnumerable<MyFolderTreeResult> folders)
        {
            // because of how folders are queried, there can be only one root
            // and it's the only one with a negative Id, which is the workspace itself
            var root = folders.Single(f => f.Id < 0);

            FolderStructure result = new FolderStructure()
            {
                Id = root.Id,
                Name = root.Name,
                Children = getChildren(root.Id)
            };

            return result;

            IEnumerable<FolderStructure> getChildren(int parentId)
            {
                return folders
                    .Where(f => f.ParentId == parentId)
                    .Select(f => new FolderStructure()
                    {
                        Id = f.Id,
                        Name = f.Name,
                        ParentId = f.ParentId,
                        Children = getChildren(f.Id)
                    });
            }
        }
    }

    /// <summary>
    /// this recursive query is in case I ever need the Level. I'm not sure I will need that,
    /// but I wanted a refresher on how to do this
    /// </summary>
    public class MyFolderTree : Query<MyFolderTreeResult>, ITestableQuery
    {
        public MyFolderTree() : base(
            @"WITH [tree] AS (
                SELECT 
                    [ws].[Id] * -1 AS [Id], [ws].[Name], 0 AS [Level], 0 AS [ParentId]
                FROM 
                    [dbo].[Workspace] [ws]                    
                WHERE 
                    [ws].[Id]=@workspaceId

                UNION ALL
                SELECT
                    [f].[Id], [f].[Name], [t].[Level]+1 AS [Level], [f].[ParentId]
                FROM 
                    [dbo].[Folder] [f]                    
                    INNER JOIN [tree] [t] ON [f].[ParentId]=[t].[Id]                
            ) SELECT * FROM [tree] ORDER BY [Level], [Name]")
        {
        }

        public int WorkspaceId { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new MyFolderTree() { WorkspaceId = 1 };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection)
        {
            return TestExecuteHelper(connection);
        }
    }
}
