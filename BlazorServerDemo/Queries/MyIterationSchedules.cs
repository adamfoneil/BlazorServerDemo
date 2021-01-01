using Dapper.QX.Abstract;
using Dapper.QX.Attributes;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;

namespace BlazorServerDemo.Queries
{
    public class MyIterationSchedules : TestableQuery<IterationSchedule>
    {
        public MyIterationSchedules() : base("SELECT * FROM [dbo].[IterationSchedule] WHERE [WorkspaceId]=@workspaceId {andWhere} ORDER BY [Name]")
        {
        }

        public int WorkspaceId { get; set; }

        [Where("[IsActive]=@isActive")]
        public bool? IsActive { get; set; }

        protected override IEnumerable<ITestableQuery> GetTestCasesInner()
        {
            yield return new MyIterationSchedules() { WorkspaceId = 1, IsActive = true };
        }
    }
}
