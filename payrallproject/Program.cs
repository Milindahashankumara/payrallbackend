using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using payrallproject.Data;
using payrallproject.Mappings;
using payrallproject.Models.Domains;
using payrallproject.Models.Helpter;
using payrallproject.Services.AuthService;
using payrallproject.Services.DepartmentService;
using payrallproject.Services.EmailServices;
using payrallproject.Services.EmployeeCategoriesService;
using payrallproject.Services.EmployeeService;
using payrallproject.Services.LoanRepaymentService;
using payrallproject.Services.LoanService;
using payrallproject.Services.OTService;
using payrallproject.Services.RolesService;
using payrallproject.Services.SalaryReportService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbstring")));

builder.Services.AddAutoMapper(typeof(MapperProfiles).Assembly);

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IOTService, OTService>();
builder.Services.AddScoped<IEmployeeCategoriesService, EmployeeCategoriesService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ISalaryReportService, SalaryReportService>();
builder.Services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();

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
