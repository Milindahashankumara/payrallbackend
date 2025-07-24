using Microsoft.Extensions.DependencyInjection;
using payrallproject.Mappings;
using payrallproject.Models.Helpter;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Services.EmailServices;
using payrallproject.Services.AuthService;
using payrallproject.Models.Domains;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbstring")));

//builder.Services.AddAutoMapper(typeof(MapperProfiles));

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();

//builder.Services.AddIdentityCore<User>()
//    .AddRoles<IdentityRole>()
//    .AddTokenProvider<DataProtectorTokenProvider<User>>("arithmos")
//    .AddEntityFrameworkStores<AuthDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;
//});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        AuthenticationType = "Jwt",
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
        );
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
