using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using RPP.BuisnessLogicContracts;
using RPP_BuisnessLogic.Implementations;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new("en-US"),
            new("ru-RU")
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

builder.Services.AddTransient<IBuyerBuisnessLogicContract, BuyerBuisnessLogicContract>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (localizeOptions is not null)
{
    app.UseRequestLocalization(localizeOptions.Value);
}

app.Run();
