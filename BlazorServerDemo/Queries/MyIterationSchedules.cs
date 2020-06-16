using Dapper.QX;
using Dapper.QX.Attributes;
using Dapper.QX.Interfaces;
using Models;
using System.Collections.Generic;
using System.Data;

namespace BlazorServerDemo.Queries
{
    public class MyIterationSchedules : Query<IterationSchedule>, ITestableQuery
    {
        public MyIterationSchedules() : base("SELECT * FROM [dbo].[IterationSchedule] WHERE [WorkspaceId]=@workspaceId {andWhere} ORDER BY [Name]")
        {
        }

        public int WorkspaceId { get; set; }

        [Where("[IsActive]=@isActive")]
        public bool? IsActive { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new MyIterationSchedules() { WorkspaceId = 1, IsActive = true };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection)
        {
            return TestExecuteHelper(connection);
        }
    }
}
