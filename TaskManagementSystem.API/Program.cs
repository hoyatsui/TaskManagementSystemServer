using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagement.Data;
using TaskManagement.Data.Repositories;
using TaskManagement.Service;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.Core.Repositories;
using TaskManagementSystem.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<TaskManagementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom Secret key for authentication"))
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Plase insert JWT with Bearer into field",
        Name="Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    var security = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id="Bearer",
                    Type=ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    };
    options.AddSecurityRequirement(security);
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TaskManagementDbContext>();
    if (!dbContext.Quotes.Any())
    {
        dbContext.Quotes.AddRange(
           
            new Quote
            {
                QuoteType = "Auto",
                Description = "Auto insurance quote 2.",
                DueDate = DateTime.Now.AddMonths(4),
                Premium = "$250",
                Sales = "Jony"
            },
        new Quote
        {
            QuoteType = "Home",
            Description = "Home insurance quote 2.",
            DueDate = DateTime.Now.AddMonths(5),
            Premium = "$350",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Life",
            Description = "Life insurance quote 2.",
            DueDate = DateTime.Now.AddMonths(6),
            Premium = "$450",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Auto",
            Description = "Auto insurance quote 3.",
            DueDate = DateTime.Now.AddMonths(7),
            Premium = "$300",
            Sales = "Jony"
        },
        new Quote
        {
            QuoteType = "Home",
            Description = "Home insurance quote 3.",
            DueDate = DateTime.Now.AddMonths(8),
            Premium = "$400",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Life",
            Description = "Life insurance quote 3.",
            DueDate = DateTime.Now.AddMonths(9),
            Premium = "$500",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Auto",
            Description = "Auto insurance quote 4.",
            DueDate = DateTime.Now.AddMonths(10),
            Premium = "$350",
            Sales = "Jony"
        },
        new Quote
        {
            QuoteType = "Home",
            Description = "Home insurance quote 4.",
            DueDate = DateTime.Now.AddMonths(11),
            Premium = "$450",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Life",
            Description = "Life insurance quote 4.",
            DueDate = DateTime.Now.AddMonths(12),
            Premium = "$550",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Auto",
            Description = "Auto insurance quote 5.",
            DueDate = DateTime.Now.AddMonths(13),
            Premium = "$400",
            Sales = "Jony"
        },
        new Quote
        {
            QuoteType = "Home",
            Description = "Home insurance quote 5.",
            DueDate = DateTime.Now.AddMonths(14),
            Premium = "$500",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Life",
            Description = "Life insurance quote 5.",
            DueDate = DateTime.Now.AddMonths(15),
            Premium = "$600",
            Sales = "Lisa"
        },
        new Quote
        {
            QuoteType = "Auto",
            Description = "Auto insurance quote 6.",
            DueDate = DateTime.Now.AddMonths(16),
            Premium = "$450",
            Sales = "Jony"
        }
        );
    dbContext.SaveChanges();
}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();

app.Run();
