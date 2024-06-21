using Ebanx.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjectionSetup();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var urls = builder.Configuration.GetSection("AllowOrigins").Get<string[]>();
var methods = builder.Configuration.GetSection("AllowMethods").Get<string[]>();

app.UseCors(policy =>
    policy.WithOrigins(urls)
    .WithMethods(methods)
    .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
