using Dapper.CX.SqlServer.Services;
using Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BlazorServerDemo.Services
{
    public class Data : SqlServerIntCrudService
    {
        public Data(string connectionString) : base(connectionString)
        {
        }

        public async Task<UserProfile> GetUserProfile(IPrincipal user)
        {
            return await GetWhereAsync<UserProfile>(new { userName = user.Identity.Name });
        }
    }
}
