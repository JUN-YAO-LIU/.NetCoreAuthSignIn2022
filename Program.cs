using Microsoft.AspNetCore.Authentication.Cookies;

try
{
    var builder = WebApplication.CreateBuilder(args);

    #region DI �`�J

    // route ��p�g
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddMvc();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        //googleOptions.CallbackPath = "/home/callback";
        //googleOptions.ClientId = "{���ε{���s��}";
        //googleOptions.ClientSecret = "{���ε{���K�_}";
    }).AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
    }); ;

    var app = builder.Build();

    #endregion DI �`�J

    #region �����h

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

    #endregion �����h

    app.Run();
}
catch (Exception ex)
{
}