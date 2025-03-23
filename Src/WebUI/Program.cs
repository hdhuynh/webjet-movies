using Webjet.Backend;
using Webjet.Infrastructure;
using Webjet.Infrastructure.Identity;
using Webjet.WebUI;
using Webjet.WebUI.Extensions;
using Webjet.WebUI.Features;
using Webjet.WebUI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUI(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

//Register IHttpClientFactory by calling AddHttpClient. TODO: remove as only background job uses it
builder.Services.AddHttpClient();

var app = builder.Build();

app
    .MapApiGroup("auth")
    .MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseRegisteredServicesPage(app.Services);

    using var scope = app.Services.CreateScope();
    
}
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionFilter();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();

app.UseSwaggerUi(settings => settings.Path = "/api");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapMovieEndpoints();

app.MapFallbackToFile("index.html");

app.Run();