using Dapper.QX;
using Dapper.QX.Interfaces;
using Models;
using System;
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
        public string FullPath { get; set; }

        public Folder ToFolder() => new Folder()
        {
            Name = Name,
            ParentId = ParentId,
            Id = Id
        };

        public override bool Equals(object obj)
        {
            var test = obj as MyFolderTreeResult;
            return (test != null) ? test.Id == Id : false;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }

    public class MyFolderTree : Query<MyFolderTreeResult>, ITestableQuery
    {
        private static string GetRecursiveRootQuery(int rootId) => (rootId < 0) ?
            @"SELECT 
                [ws].[Id] * -1 AS [Id], [ws].[Name], 0 AS [Level], 0 AS [ParentId], CAST([ws].[Name] as varchar(255)) AS [FullPath]
            FROM 
                [dbo].[Workspace] [ws]  
            WHERE
                [ws].[Id]=@rootId * -1" :
            @"SELECT
                [f].[Id], [f].[Name], 0 AS [Level], [f].[ParentId], CAST([f].[Name] AS varchar(255)) AS [FullPath]
            FROM
                [dbo].[Folder] [f]
            WHERE
                [f].[Id]=@rootId";

        public static string GetRecursiveQuery(int rootId) =>
            $@"{GetRecursiveRootQuery(rootId)} 
            UNION ALL 
            SELECT
                [f].[Id], [f].[Name], [t].[Level]+1 AS [Level], [f].[ParentId], CAST(CONCAT([t].[FullPath], ' / ', [f].[Name]) as varchar(255)) AS [FullPath]
            FROM 
                [dbo].[Folder] [f]                    
                INNER JOIN [tree] [t] ON [f].[ParentId]=[t].[Id]";

        public MyFolderTree(int rootId) : base(
            $@"WITH [tree] AS (
                {GetRecursiveQuery(rootId)}
            ) SELECT * FROM [tree] ORDER BY [FullPath]")
        {
            RootId = rootId;
        }

        public int RootId { get; private set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new MyFolderTree(-1);
            yield return new MyFolderTree(1);
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection) => TestExecuteHelper(connection);
    }

    /// <summary>
    /// ended up not needing this, but it shows example of building hierarchy recursively from query results
    /// </summary>
    [Obsolete]
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

}
