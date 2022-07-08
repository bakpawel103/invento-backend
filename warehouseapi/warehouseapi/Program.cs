using Microsoft.OpenApi.Models;
using System.Reflection;
using warehouseapi.Models;
using warehouseapi.Repositories;
using warehouseapi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Warehouse API",
        Description = "An ASP.NET Core Web API for managing warehouse items",
        Contact = new OpenApiContact
        {
            Name = "Paweł Bąk",
            Url = new Uri("https://github.com/bakpawel103")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<IService<Item>, ItemsService>();
builder.Services.AddScoped<IRepository<Item>, ItemsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.Run();
