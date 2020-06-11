using Dapper.CX.SqlServer.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System.Threading.Tasks;

namespace BlazorServerDemo.Services
{
    public class Data : SqlServerIntCrudService
    {
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

            await SaveAsync(updateProfile, new string[]
            {
                nameof(UserProfile.DisplayName),
                nameof(UserProfile.TimeZoneId)
            });
        }
    }
}
