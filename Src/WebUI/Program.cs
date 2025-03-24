using Webjet.Backend;
using Webjet.WebUI;
using Webjet.WebUI.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUI(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    using var scope = app.Services.CreateScope();
}
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();

app.UseSwaggerUi(settings => settings.Path = "/api");

app.UseRouting();
app.MapMovieEndpoints();
app.MapFallbackToFile("index.html");

app.Run();