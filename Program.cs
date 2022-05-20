using Microsoft.AspNetCore.Authentication.Cookies;

try
{
    var builder = WebApplication.CreateBuilder(args);

    #region DI 注入

    // route 轉小寫
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddMvc();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
        .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        //googleOptions.CallbackPath = "/home/callback";
        //googleOptions.ClientId = "{應用程式編號}";
        //googleOptions.ClientSecret = "{應用程式密鑰}";
    })
        .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
    }).AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
    }); ;

    //    dotnet user-secrets set "Authentication:Facebook:AppId" "1082788795450871"
    //dotnet user-secrets set "Authentication:Facebook:AppSecret" "b887fd11429c6833c9e47ce1a1fed26e"
    // facebook AppId2 1082788795450871
    // facebook secrit b887fd11429c6833c9e47ce1a1fed26e

    var app = builder.Build();

    #endregion DI 注入

    #region 中介層

    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });

    #endregion 中介層

    app.Run();
}
catch (Exception ex)
{
}