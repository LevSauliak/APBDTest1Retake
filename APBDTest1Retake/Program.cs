using APBDTest1Retake.Repositories;
using APBDTest1Retake.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbRepository, DbRepository>();
builder.Services.AddScoped<IClientsService, ClientsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();