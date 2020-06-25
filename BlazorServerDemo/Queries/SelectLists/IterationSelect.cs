using Dapper.QX;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorServerDemo.Queries.SelectLists
{
    public class IterationSelect : Query<SelectListItem>
    {
        public IterationSelect() : base("")
        {

        }
    }
}
