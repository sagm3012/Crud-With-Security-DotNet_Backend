using Auth1.Core.Interface;
using Auth1.Core.Service;
using Auth1.Data;
using Microsoft.EntityFrameworkCore;
using Auth1.Authorition;
using Auth1.Helpers;
using Auth1.Core.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    builder => { builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); }));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowOrigin",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(
                    "http://localhost:4200");
        });
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    /// option.Password.RequiredLength = 1;
     option.Password.RequireLowercase = false;
     option.Password.RequireUppercase = false;
     option.Password.RequireNonAlphanumeric = false;
     option.Password.RequireDigit = false;
     option.Lockout.AllowedForNewUsers = false;
     option.SignIn.RequireConfirmedAccount = true;
})
               .AddEntityFrameworkStores<AuthDbContext>()
               .AddDefaultTokenProviders();

//builder.Services.AddDefaultIdentity<ApplicationUser>(opt =>
//{
//    opt.SignIn.RequireConfirmedAccount = true;
//}).AddRoles<ApplicationRole>().AddEntityFrameworkStores<AuthDbContext>();

//builder.Services.AddIdentity
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:secret").Value!)),
        };
    });
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
//builder.Services.AddSingleton<HttpContext, HttpContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();
app.UseCors("AllowOrigin"); 

app.Run();