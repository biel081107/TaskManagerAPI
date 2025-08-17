using Notiom.Controllers;
using Notiom.Data;
using Notiom.Services;
using Notiom.Repositories;
using Notiom.Settings;
using Microsoft.EntityFrameworkCore;
using Notiom.Repositories.Tarefas;
using Notiom.Repositories.Usuarios;
using Notiom.Services.Tarefas;
using Notiom.Services.Usuarios;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["JwtSettings:SecretKey"];
if (jwtKey == null)
{
    throw new InvalidOperationException("JWT key is not configured.");
}
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);


builder.Services.AddOpenApi();

// Add JwtAuthentication
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.AddScoped<JwtService>();

//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Notiom Tasks API",
        Version = "v1",
        Description = "A professional API for managing tasks, inspired by Notion. Provides endpoints for user authentication, task management, and more.",
    });

    // Enable JWT Bearer authentication in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the format: Bearer {your_token}"
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
            Array.Empty<string>()
        }
    });
});
//Add Controllers
builder.Services.AddControllers();

// Add Repositories
builder.Services.AddScoped<IloginRepository, LoginRepository>();
builder.Services.AddScoped<ITarefasRepositories, TarefasRepositories>();
builder.Services.AddScoped<IUsuariosRepositories, UsuariosRepositories>();

//Add Services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITarefasService, TarefasService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();

//Add Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

// Add Authorization
builder.Services.AddAuthorization();

// Add DbContext
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



