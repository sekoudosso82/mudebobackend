using BusinessLayer;
using DbLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "backend", Version = "v1" });
});

// added by me 
builder.Services.AddCors((options) =>
{
    options.AddPolicy(name: "dev", builder =>
    {
        builder.WithOrigins(
            "https://localhost:7097",
            "http://localhost:5243",
            "http://localhost:41008",
            "http://localhost:5243",
            "http://localhost:5243",
            "https://localhost:5243",
            "https://localhost:44373",
            "http://localhost:44373",
            "https://localhost:7029"

        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MudeboDb>(options =>
{
    if ( !options.IsConfigured)
    {
        options.UseSqlServer("Server=LAPTOP-Q18VFCDI\\SQLEXPRESS;Database=MudeboDb;Trusted_Connection=True;Encrypt=False;");
    }
});

builder.Services.AddScoped<IActivities, ActivitiesService>();
builder.Services.AddScoped<IMembers, MembersService>();
builder.Services.AddScoped<IProjects, ProjectsService>();
builder.Services.AddScoped<ILogins, LoginsService>();
builder.Services.AddControllersWithViews();

///////////////////////////////////////////////////
var key = "this is my custom secret key for authentication";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
    };
});
builder.Services.AddAuthorization();
/////////////////////////////////////////////////////////////////

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "backend v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("dev");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Members}/action=MembersList"
    
);

app.Run();
