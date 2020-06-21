using BlazorServerDemo.Queries;
using Dapper.CX.SqlServer.Services;
using Dapper.QX;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using System;
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

        private UserProfile _userProfile;
        public async Task<int> GetWorkspaceIdAsync()
        {
            if (_userProfile is null) _userProfile = await GetUserProfileAsync();
            return _userProfile.WorkspaceId ?? 0;
        }

        public async Task<Workspace> GetWorkspaceAsync()
        {
            var wsId = await GetWorkspaceIdAsync();
            return (wsId != 0) ? await GetAsync<Workspace>(wsId) : default;            
        }
        
        public async Task<UserProfile> GetUserProfileAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var result = await GetWhereAsync<UserProfile>(new { userName = authState.User.Identity.Name });
            return result;
        }

        public async Task UpdateUserProfileAsync(UserProfile userProfile)
        {
            var updateProfile = await GetUserProfileAsync();

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

        public async Task<IEnumerable<Workspace>> GetMyWorkspacesAsync()
        {
            var profile = await GetUserProfileAsync();

            using (var cn = GetConnection())
            {
                return await new MyWorkspaces() { UserId = profile.UserId }.ExecuteAsync(cn);
            }            
        }

        public async Task<int> SaveAsync<TModel>(TModel model)
        {
            if (_userProfile is null) _userProfile = await GetUserProfileAsync();
            return await SaveAsync(model, user: _userProfile);
        }

        public async Task<int> SaveAsync<TModel>(TModel model, Action<Exception> onException, Func<Task> onSuccess = null)
        {
            try
            {
                var result = await SaveAsync(model);
                if (onSuccess != null) await onSuccess();
                return result;
            }
            catch (Exception exc)
            {
                onException.Invoke(exc);
                return default;
            }
        }

        public async Task<Result> TrySaveAsync<TModel>(TModel model)
        {
            if (_userProfile is null) _userProfile = await GetUserProfileAsync();
            return await TrySaveAsync(model, user: _userProfile);
        }

        public async Task DeleteAsync<TModel>(TModel model, Action<Exception> onException, Action<TModel> onSuccess = null)
        {
            try
            {
                if (_userProfile is null) _userProfile = await GetUserProfileAsync();
                await DeleteAsync(model, user: _userProfile);
                onSuccess?.Invoke(model);
            }
            catch (Exception exc)
            {
                onException.Invoke(exc);
            }
        }

        public async Task DeleteAsync<TModel>(TModel model, int id, Action<Exception> onException, Action<TModel> onSuccess = null)
        {
            try
            {
                if (_userProfile is null) _userProfile = await GetUserProfileAsync();
                await DeleteAsync<TModel>(id, user: _userProfile);
                onSuccess?.Invoke(model);
            }
            catch (Exception exc)
            {
                onException.Invoke(exc);
            }
        }

        public async Task<int> MergeAsync<TModel>(TModel model)
        {
            var user = await GetUserProfileAsync();
            return await MergeAsync(model, user: user);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(Query<T> query)
        {
            using (var cn = GetConnection())
            {
                return await query.ExecuteAsync(cn);
            }
        }
    }
}
