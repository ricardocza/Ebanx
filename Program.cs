using Ebanx.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Insert(0, new ResponseFormater());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjectionSetup();
var app = builder.Build();

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
