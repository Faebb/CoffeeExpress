using CoffeeExpress.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ============================
// CONFIGURACIÓN DE SERVICIOS
// ============================

// Conexión a la base de datos
builder.Services.AddDbContext<CoffeeEpxpressDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeExpressConnection")));

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "CoffeeExpressAPI",
            ValidAudience = "CoffeeExpressAPI",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("webos-con-aceite"))
        };
    });

// Configuración de Swagger para documentación
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoffeeExpress API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese 'Bearer {token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new List<string>() }
    });
});

// CORS para permitir acceso desde clientes móviles
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMobileApp",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Agregar controladores
builder.Services.AddControllers();

// ============================
// CREACIÓN DE LA APLICACIÓN
// ============================
var app = builder.Build();

// ============================
// CONFIGURACIÓN DE MIDDLEWARES
// ============================

// Manejo de errores global
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Ocurrió un error inesperado.");
    });
});

// Seguridad y configuración HTTPS
app.UseHttpsRedirection();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Habilitar CORS
app.UseCors("AllowMobileApp");

// ============================
// CONFIGURACIÓN DEL PIPELINE
// ============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
