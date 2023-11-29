using DoctorAi.Application;
using DoctorAi.Domain._01.Entities._02.Verifycode;
using DoctorAi.Domain._01.Entities.Users;
using DoctorAi.Infrastructure._01.JwtService;
using DoctorAi.Infrastructure._02.UserService;
using DoctorAi.Infrastructure._03.SmsService;
using DoctorAi.Persistance._01.Contexts;
using DoctorAi.Persistance._02.Users;
using DoctorAi.Persistance._03.VerifyCodes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoctorAiDbContext>(p =>
{
    p.UseSqlite(builder.Configuration["ConnectionStrings:Database1"]);
    p.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    p.EnableSensitiveDataLogging();
}, ServiceLifetime.Singleton);
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<KavenegarConfig>(builder.Configuration.GetSection("Kavenegarinfo"));

//builder.Services.AddDbContext<DoctorAiDbContext>(o =>
//{
//    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(""));
//    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//    o.EnableSensitiveDataLogging();
//});
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ClockSkew = TimeSpan.Zero,

         ValidAudience =builder.Configuration ["JWT:ValidAudience"],
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
     };
 });
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVerifyCodeRepository,VerifyCodeRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ISmsService,SmsService>();
builder.Services.AddScoped<ITokenService,TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
