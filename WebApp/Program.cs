using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

var supportedCultureCodes= new[] { "tr-TR", "en-US" };
//var supportedCultureCodes = new[] { "tr", "en" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultureCodes[0])
    .AddSupportedCultures(supportedCultureCodes)
    .AddSupportedUICultures(supportedCultureCodes);

var builder = WebApplication.CreateBuilder(args);

// Add ocalization
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo(supportedCultureCodes[0]),
        new CultureInfo(supportedCultureCodes[1])
    };

    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultureCodes[0], uiCulture: supportedCultureCodes[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
    {
        // My custom request culture logic
        return await Task.FromResult(new ProviderCultureResult(supportedCultureCodes[0]));
    }));
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
