using Dapper.QX;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class FolderSelect : Query<SelectListItem>
    {
        public FolderSelect() : base(
            $@"WITH [tree] ({MyFolderTree.RecursiveQuery}) 
            SELECT [t].[ID] AS [Value], [t].[FullPath] AS [Text]
            FROM [tree] [t]")
        {
        }

        public int WorkspaceId { get; set; }
    }
}
