using GameBookASP.Data;
using GameBookASP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddControllers();

// Configure Identity and API Endpoints together
builder.Services.AddIdentityApiEndpoints<User>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = false;
    opt.SignIn.RequireConfirmedEmail = false;
})
.AddRoles<Role>()  // Add support for roles
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization(c =>
{
    c.AddPolicy("Admin", p => p.RequireRole("Admin"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // max request 50 MB 
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 12 * 1024 * 1024;  // 12 MB per form
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback);
    });
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => new Uri(origin).IsLoopback));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/user").MapCustomIdentityApi<User>();
app.MapControllers();

// Add SPA fallback routing to handle client-side routing
app.MapFallbackToFile("index.html");

app.Run();