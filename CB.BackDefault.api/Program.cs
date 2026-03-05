using CB.BackDefault.Api.Configurations;
using CB.BackDefault.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
//builder.Services.AddOpenApi();


// Adiciona e configura o Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDependencyInjection(builder.Configuration);

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
