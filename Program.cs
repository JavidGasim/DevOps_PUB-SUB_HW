using DevOps_PUB_SUB_HW.Services.Abstracts;
using DevOps_PUB_SUB_HW.Services.Concretes;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IConnectionMultiplexer>(r =>
ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { { "xxxxxxxxxxxxxxxxxxxx", 00000 } },
    User = "xxxxxxxxxxx",
    Password = "xxxxxxxxxxxxxxx"
}));

builder.Services.AddScoped<IChannelService, ChannelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
