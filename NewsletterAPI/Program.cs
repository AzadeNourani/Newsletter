using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewsletterAPI.Data.Contexts;
using System.Configuration;
using NewsletterAPI.Data.Services;
using NewsletterAPI.Data.Services.Queries.GetPersonnelList;
using NewsletterAPI.Data.Services.Queries.GetLastNews;
using NewsletterAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("NewsletterDbContext")));
builder.Services.AddHangfireServer();
string connString = builder.Configuration.GetConnectionString("NewsletterDbContext");

builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseSqlServer(connString));

builder.Services.AddScoped<IGetPersonnelListService, GetPersonnelListService>();
builder.Services.AddScoped<ISendNewsToPersonnelListService, SendNewsToPersonnelListService>();
builder.Services.AddScoped<IGetLastNewsService, GetLastNewsService>();
builder.Services.AddScoped<SendNewsletterJob>();



void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
{
    // Other app configurations...

    // Run the SendNewsToPersonnelListService every one minute
    RecurringJob.AddOrUpdate<SendNewsToPersonnelListService>("SendNewsToPersonnelList",
        service => service.ExecuteAsync(), "*/1 * * * *");

    // Other app configurations...
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHangfireServer();

 // Starting Service Automaticly
var serviceProvider = builder.Services.BuildServiceProvider();

app.UseAuthorization();
app.UseHangfireDashboard();
app.MapControllers();

app.Run();
