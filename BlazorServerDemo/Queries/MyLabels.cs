using Dapper.QX;
using Models;

namespace BlazorServerDemo.Queries
{
    public class MyLabels : Query<Label>
    {
        public MyLabels() : base(
            @"SELECT 
                [lbl].* 
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
    }
}
