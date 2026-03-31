using BusinessLayer;
using DbLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
            "http://localhost:4200",
            "https://localhost:7029"

        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MudeboDb>(options =>
{
    if ( options.IsConfigured)
    {
        options.UseSqlServer("Server=LAPTOP-Q18VFCDI\\SQLSERVERFORDEV;Database=CodeFirstJokeApp;Trusted_Connection=True;");
    }
});

builder.Services.AddScoped<IActivities, ActivitiesService>();
builder.Services.AddScoped<IMembers, MembersService>();
builder.Services.AddScoped<IProjects, ProjectsService>();
builder.Services.AddScoped<ILogins, LoginsService>();
builder.Services.AddControllersWithViews();

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
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Members}/action=MembersList"
    
    );

app.Run();
