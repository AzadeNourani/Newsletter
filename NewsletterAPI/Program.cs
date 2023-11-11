using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewsletterAPI.Data.Contexts;
using System.Configuration;
using NewsletterAPI.Data.Services;
using NewsletterAPI.Data.Services.Queries.GetPersonnelList;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<NewsDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("NewsletterDbContext") ?? throw new InvalidOperationException("Connection string 'NewsletterDbContext' not found.")));



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("NewsletterDbContext")));
//builder.Services.AddScoped<INewsletterService>();
string connString = builder.Configuration.GetConnectionString("NewsletterDbContext");

builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseSqlServer(connString));

builder.Services.AddScoped<IGetPersonnelListService, GetPersonnelListService>();
builder.Services.AddScoped<ISendNewsToPersonnelListService, SendNewsToPersonnelListService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();
app.UseHangfireServer();

 // Starting Service Automaticly
var serviceProvider = builder.Services.BuildServiceProvider();

app.UseAuthorization();

app.MapControllers();

app.Run();
