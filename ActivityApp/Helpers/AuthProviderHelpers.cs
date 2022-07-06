using Microsoft.AspNetCore.Components.Authorization;

namespace ActivityApp.Helpers
{
    public static class AuthProviderHelpers
    {
        public static async Task<UserModel> GetUsrFromAuthentication(this AuthenticationStateProvider authProvider, IUserData usrData)
        {
            var authStae = await authProvider.GetAuthenticationStateAsync();
            var objId = authStae.User.Claims.FirstOrDefault(cl => cl.Type.Contains("objectidentifier"))?.Value;
            return await usrData.GetUserFromAuth(objId);
        }
    }
}
