using BlazorServerDemo.Queries;
using Dapper.QX;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void MyLabelsQuery()
        {
            QueryHelper.Test<MyLabels>(GetConnection);
        }

        [TestMethod]
        public void MyWorkspacesQuery()
        {
            QueryHelper.Test<MyWorkspaces>(GetConnection);
        }
    }
}
