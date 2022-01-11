using FluentValidation;
using Geekiam.Data;
using Geekiam.Data.Services;
using Geekiam.Database;
using Geekiam.Domain.Requests.Posts;
using Geekiam.Domain.Responses.Posts;
using Geekiam.Posts.Service.Behaviours;
using Geekiam.Posts.Service.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Threenine.Data.DependencyInjection;


namespace Geekiam.Posts.Service;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Geekiam.Posts.Service", Version = "v1" });
            c.CustomSchemaIds(x => x.FullName);
            c.EnableAnnotations();
        });

        services.AddDbContext<GeekiamContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("articles"))
        ).AddUnitOfWork<GeekiamContext>();
            
        services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        services.AddMediatR(typeof(Startup))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddTransient<IDataService<Submission, Submitted>, SubmitArticleDataService>();
        services.AddAutoMapper(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSerilogRequestLogging();
            
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<GeekiamContext>();
            context?.Database.Migrate();
        }
            
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geekiam.Posts.Service v1"));
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}