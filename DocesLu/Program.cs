using DocesLu.Data;
using DocesLu.Model.Doces;
using DocesLu.Model.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddDbContext<ConnectionContext>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IDocesRepository, DocesRepository>();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.AllowAnyOrigin()   // Permite acesso de qualquer domínio
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// JWT Authentication
var key = Encoding.ASCII.GetBytes(DocesLu.Key.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// CORS
app.UseCors("AllowReactApp");

// Arquivos estáticos (imagens)
var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
if (!Directory.Exists(storagePath))
    Directory.CreateDirectory(storagePath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(storagePath),
    RequestPath = "/Storage"
});

// Autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Executa a aplicação
app.Run();
