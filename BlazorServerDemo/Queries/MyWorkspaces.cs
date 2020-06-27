using Dapper.QX;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries
{
    public class MyWorkspaces : Query<Workspace>, ITestableQuery
    {
        public MyWorkspaces() : base(
            @"SELECT 
                [ws].* 
            FROM 
                [dbo].[Workspace] [ws]
            WHERE 
                EXISTS(SELECT 1 FROM [dbo].[WorkspaceUser] WHERE [WorkspaceId]=[ws].[Id] AND [UserId]=@userId AND [IsEnabled]=1) OR
                [ws].[OwnerUserId]=@userId
            ORDER BY
                [ws].[Name]")
        {
        }

        public int UserId { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new MyWorkspaces() { UserId = 1 };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection) => TestExecuteHelper(connection);
    }
}
