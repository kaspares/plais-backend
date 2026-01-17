using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Plais.Data.Interfaces;
using Plais.Data.Repositories;
using Plais.Data.Seeders;
using Plais.Middlewares;
using Plais.Models;
using Plais.Services;
using Plais.Services.Interfaces;
using PLAIS.API.Data;
using Serilog;
using System.Net;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            },
            []
        }
    });
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PlaisDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".plais.auth";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IByLawsSeeder, ByLawsSeeder>();
builder.Services.AddScoped<IHistorySeeder, HistorySeeder>();
builder.Services.AddScoped<IMainPageTextsSeeder, MainPageTextsSeeder>();
builder.Services.AddScoped<IBulletinServices, BulletinServices>();
builder.Services.AddScoped<IMemberServices, MemberServices>();
builder.Services.AddScoped<IFoundingMemberService, FoundingMemberService>();
builder.Services.AddScoped<ICadenceService, CadenceService>();
builder.Services.AddScoped<IExecutiveMemberService, ExecutiveMemberService>();
builder.Services.AddScoped<IEventGroupService, EventGroupService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IByLawsService, ByLawsService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IResourceCategoryService, ResourceCategoryService>();
builder.Services.AddScoped<IResourceGroupService, ResourceGroupService>();
builder.Services.AddScoped<IResourceItemService, ResourceItemService>();
builder.Services.AddScoped<IMainPageTextService, MainPageTextService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();
builder.Services.AddScoped<IMainPageCarouselService, MainPageCarouselService>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
builder.Services.AddScoped<IBulletinRepository, BulletinRepository>();
builder.Services.AddScoped<IEventGroupRepository, EventGroupRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IByLawsRepository, ByLawsRepository>();
builder.Services.AddScoped<ICustomHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IExecutiveMemberRepository, ExecutiveMemberRepository>();
builder.Services.AddScoped<ICadenceRepository, CadenceRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IFoundingMemberRepository, FoundingMemberRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IResourceCategoryRepository, ResourceCategoryRepository>();
builder.Services.AddScoped<IResourceGroupRepository, ResourceGroupRepository>();
builder.Services.AddScoped<IResourceItemRepository, ResourceItemRepository>();
builder.Services.AddScoped<IMainPageTextsRepository, MainPageTextsRepository>();
builder.Services.AddScoped<IMainPageCarouselRepository, MainPageCarouselRepository>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddDbContext<PlaisDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(Program));

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("https://localhost:3000", "http://localhost:3000")
            .AllowCredentials();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(opts =>
{
    opts.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    opts.KnownNetworks.Clear();
    opts.KnownProxies.Clear();
    opts.KnownProxies.Add(IPAddress.Loopback);
});


var app = builder.Build();

// Configure the HTTP request pipeline.


var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<PlaisDbContext>();

await db.Database.MigrateAsync();

var byLawsSeeder = scope.ServiceProvider.GetRequiredService<IByLawsSeeder>();
var historySeeder = scope.ServiceProvider.GetRequiredService<IHistorySeeder>();
var mainPageTextSeeder = scope.ServiceProvider.GetRequiredService<IMainPageTextsSeeder>();
await byLawsSeeder.SeedAsync();
await historySeeder.SeedAsync();
await mainPageTextSeeder.SeedAsync();

app.UseSerilogRequestLogging();

app.UseForwardedHeaders();

if (app.Environment.IsEnvironment("Testing"))
{
    app.Use(async (ctx, next) =>
    {
        ctx.User = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.NameIdentifier, "test-user") },
                "TestAuth"));

        await next();
    });
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //CORS
    app.UseCors("DevCors");
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // dla wwwroot

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }