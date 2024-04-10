using CompanyName.DDD.API.Extensions;
using CompanyName.DDD.API.Middleware;
using CompanyName.DDD.Domain.Common.EFCore;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Operations"),
        sqlServerOptionsAction: builder =>
        {
            builder.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });
});
DIExtension.AddConfigurations(builder.Services);
OptionsExtension.AddConfiguration(builder.Services, builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.EnvironmentName.Equals("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
Console.WriteLine("Server started");
app.Run();