using BlazorServerDemo.Queries;
using BlazorServerDemo.Queries.SelectLists;
using Dapper.QX;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Queries;

namespace Testing
{
    [TestClass]
    public class QueryTests
    {
        private readonly IConfiguration _config;

        public QueryTests()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        private SqlConnection GetConnection()
        {
            string connectionStr = _config.GetSection("ConnectionStrings").GetValue<string>("Default");
            return new SqlConnection(connectionStr);
        }

        [TestMethod]
        public void MyLabelsQuery() => QueryHelper.Test<MyLabels>(GetConnection);

        [TestMethod]
        public void MyWorkspacesQuery() => QueryHelper.Test<MyWorkspaces>(GetConnection);

        [TestMethod]
        public void MyActivitiesQuery() => QueryHelper.Test<MyActivities>(GetConnection);

        [TestMethod]
        public void MyIterationSchedulesQuery() => QueryHelper.Test<MyIterationSchedules>(GetConnection);

        [TestMethod]
        public void MyFolderTreeQuery() => QueryHelper.Test<MyFolderTree>(GetConnection);
        
        [TestMethod]
        public void CountWorkItemsByFolderQuery() => QueryHelper.Test<CountWorkItemsByFolder>(GetConnection);

        [TestMethod]
        public void FolderSelectQuery() => QueryHelper.Test<FolderSelect>(GetConnection);

        [TestMethod]
        public void RebuildWorkItemLabelsQuery() => QueryHelper.Test<RebuildWorkItemLabels>(GetConnection);

        [TestMethod]
        public void OpenWorkItemsQuery() => QueryHelper.Test<OpenWorkItems>(GetConnection);
    }
}
