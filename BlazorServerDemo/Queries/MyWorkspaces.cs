using Dapper.QX;
using Models;

namespace BlazorServerDemo.Queries
{
    public class MyWorkspaces : Query<Workspace>
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
    }
}
