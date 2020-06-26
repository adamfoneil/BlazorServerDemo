using Dapper.QX;
using Dapper.QX.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Models.Queries
{
    public class WorkItemLabels : Query<int>, ITestableQuery
    {
        public WorkItemLabels() : base("SELECT [LabelId] FROM [dbo].[WorkItemLabel] WHERE [WorkItemId]=@workItemId")
        {
        }

        public int WorkItemId { get; set; }

        public IEnumerable<ITestableQuery> GetTestCases()
        {
            yield return new WorkItemLabels() { WorkItemId = 1 };
        }

        public IEnumerable<dynamic> TestExecute(IDbConnection connection)
        {
            return TestExecuteHelper(connection);
        }
    }
}
