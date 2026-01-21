using PetRegister.Infrastructure;
using PetRegister.Infrastructure.Caching;
using PetRegister.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add repositories/dbbcontext/services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ICachingService, CachingService>();

builder.Services.AddStackExchangeRedisCache(r =>
{
    r.InstanceName = "instance";
    r.Configuration = "localhost:6379";
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
