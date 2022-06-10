using Api.Behaviours;
using Api.Content.Middleware;
using FluentValidation;
using Geekiam.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Threenine;
using Threenine.Data.DependencyInjection;
using Threenine.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"});
    c.CustomSchemaIds(x => x.FullName);
    c.EnableAnnotations();
});  

builder.Services.AddDbContext<GeekiamContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("articles"))
).AddUnitOfWork<GeekiamContext>();
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(Program))
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
           
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}
app.UseHttpsRedirection();

            
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<GeekiamContext>();
    context?.Database.Migrate();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
