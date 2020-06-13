using BlazorServerDemo.Queries;
using Dapper.CX.SqlServer.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.SqlServer.Management.Smo;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServerDemo.Services
{
    public class Data : SqlServerIntCrudService
    {
        private int _workspaceId;

        public Data(string connectionString) : base(connectionString)
        {
        }        

        public async Task<UserProfile> GetUserProfile(AuthenticationStateProvider authenticationStateProvider)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return await GetWhereAsync<UserProfile>(new { userName = authState.User.Identity.Name });
        }

        public async Task UpdateUserProfile(AuthenticationStateProvider authenticationStateProvider, UserProfile userProfile)
        {
            var updateProfile = await GetUserProfile(authenticationStateProvider);

            updateProfile.DisplayName = userProfile.DisplayName;
            updateProfile.TimeZoneId = userProfile.TimeZoneId;
            updateProfile.WorkspaceId = userProfile.WorkspaceId;

            await SaveAsync(updateProfile, new string[]
            {
                nameof(UserProfile.DisplayName),
                nameof(UserProfile.TimeZoneId),
                nameof(UserProfile.WorkspaceId)
            });
        }

        public async Task<IEnumerable<Workspace>> GetMyWorkspaces(AuthenticationStateProvider authenticationStateProvider)
        {
            var profile = await GetUserProfile(authenticationStateProvider);

            using (var cn = GetConnection())
            {
                return await new MyWorkspaces() { UserId = profile.UserId }.ExecuteAsync(cn);
            }            
        }
    }
}
