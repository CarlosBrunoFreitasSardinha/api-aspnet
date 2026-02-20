using CB.BackDefault.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();


// Adiciona e configura o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddDependencyInjection(builder.Configuration);

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    // Habilita o Swagger e a UI interativa
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
