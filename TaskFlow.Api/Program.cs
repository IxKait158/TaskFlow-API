using Microsoft.AspNetCore.CookiePolicy;
using TaskFlow.Api.Extensions;
using TaskFlow.Api.Middleware;
using TaskFlow.Application.Common.Extensions;
using TaskFlow.Infrastructure.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

services.AddHttpLogging();
services.AddHttpContextAccessor();

services.AddSwaggerGen(c => c.UseInlineDefinitionsForEnums());

services.AddApplication();
services.AddInfrastructure(builder.Configuration);

services.AddApiAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();