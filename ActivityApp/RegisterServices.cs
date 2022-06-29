namespace ActivityApp
{
    public static class RegisterServices
    {
        public static void ConfigureDI(WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
        }
    }
}
