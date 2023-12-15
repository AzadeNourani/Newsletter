using Microsoft.EntityFrameworkCore;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewsletterAPI.Data.Contexts;
using System.Configuration;
using NewsletterAPI.Data.Services;
using NewsletterAPI.Data.Services.Queries.GetPersonnelList;
using NewsletterAPI.Data.Services.Queries.GetLastNews;
using NewsletterAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NewsletterAPI.Contracts;
using NewsletterAPI.Services;
using Microsoft.AspNetCore.Identity;
using NewsletterAPI.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Jwt Service Configure
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constansts.JWT_SECURITY_KEY_FOR_TOKEN)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("NewsletterDbContext")));
builder.Services.AddHangfireServer();
string connString = builder.Configuration.GetConnectionString("NewsletterDbContext");

builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseSqlServer(connString));
builder.Services.AddScoped<ILoggerService, LoggerService>();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>();
//.AddEntityFramework<NewsletterDbContext>()
//  .AddDefaultTokenProviders();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<NewsletterDbContext>();
//builder.Services
//    .AddIdentity<IdentityUser, IdentityRole>()
//    .AddUserStore<ApplicationUserStore>()
//    .AddEntityFrameworkStores<NewsletterDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//       .AddEntityFrameworkStores<NewsletterDbContext>()
//       .AddDefaultTokenProviders();

builder.Services.AddScoped<IGetPersonnelListService, GetPersonnelListService>();

builder.Services.AddScoped<IGetLastNewsService, GetLastNewsService>();
builder.Services.AddScoped<ISendNewsToPersonnelListService, SendNewsToPersonnelListService>();
builder.Services.AddScoped<SendNewsletterJob>();



void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
{
    //Run the SendNewsToPersonnelListService every five minute
    RecurringJob.AddOrUpdate<SendNewsToPersonnelListService>("SendNewsToPersonnelList",
        service => service.ExecuteAsync(), "*/5 * * * *");
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
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard();
app.MapControllers();

app.Run();
