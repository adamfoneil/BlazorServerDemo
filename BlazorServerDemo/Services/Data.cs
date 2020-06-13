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
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public Data(string connectionString, AuthenticationStateProvider authenticationStateProvider) : base(connectionString)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<UserProfile> GetUserProfile()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            return await GetWhereAsync<UserProfile>(new { userName = authState.User.Identity.Name });
        }

        public async Task UpdateUserProfile(UserProfile userProfile)
        {
            var updateProfile = await GetUserProfile();

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

        public async Task<IEnumerable<Workspace>> GetMyWorkspaces()
        {
            var profile = await GetUserProfile();

            using (var cn = GetConnection())
            {
                return await new MyWorkspaces() { UserId = profile.UserId }.ExecuteAsync(cn);
            }            
        }
    }
}
