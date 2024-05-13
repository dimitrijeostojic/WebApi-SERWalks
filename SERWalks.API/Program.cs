using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SERWalks.API.Data;
using SERWalks.API.Mappings;
using SERWalks.API.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Dodata autorizacija u swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gde Izaci API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
        In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

});
builder.Services.AddDbContext<SERWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SERWalksConnectionString")));
builder.Services.AddDbContext<SERWalksAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SERWalksAuthConnectionString")));

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepositoy>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>() // Konfiguracija Identity servisa
    .AddRoles<IdentityRole>() // Dodavanje podrške za role
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("SERWalks") // Dodavanje token provajdera
    .AddEntityFrameworkStores<SERWalksAuthDbContext>() // Podešavanje Entity Framework skladista
    .AddDefaultTokenProviders(); // Dodavanje podrazumevanih token provajdera

// Konfiguracija opcija identiteta
builder.Services.Configure<IdentityOptions>(options =>
{
    // Postavke za lozinku korisnika
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

#region JWTAuthorization
// Dodavanje podrške za JWT autentikaciju
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});
#endregion


#region CORS
//OVO SLUZI ZA CORS 
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder => builder
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader()
//            .WithExposedHeaders("Content-Disposition")); // Dodajte ovo ako je potrebno
//}); 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
//    RequestPath = "/Images"
//});

app.MapControllers();

app.Run();
