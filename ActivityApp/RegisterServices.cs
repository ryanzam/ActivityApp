namespace ActivityApp
{
    public static class RegisterServices
    {
        public static void ConfigureDI(WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<IDbConnection, DbConnection>();
            builder.Services.AddSingleton<IUserData, UserData>();
            builder.Services.AddSingleton<IProposalData, ProposalData>();
            builder.Services.AddSingleton<IStatusData, StatusData>();
            builder.Services.AddSingleton<IActivityData, ActivityData>();

        }
    }
}
