using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace ActivityApp
{
    public static class RegisterServices
    {
        public static void ConfigureDI(WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
            builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IDbConnection, DbConnection>();
            builder.Services.AddSingleton<IUserData, UserData>();
            builder.Services.AddSingleton<IProposalData, ProposalData>();
            builder.Services.AddSingleton<IStatusData, StatusData>();
            builder.Services.AddSingleton<IActivityData, ActivityData>();

            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureB2C"));
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim("jobTitle", "Admin");
                });
            });
        }
    }
}
