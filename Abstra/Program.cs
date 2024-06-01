using Abstra.Injections;
using Abstra.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Abstra
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            // Inyecciones de la API
            AutoInjection.Configure(builder);

            // Mapeos con Mapster
            MappingConfigurator.Configure();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("Dev",
                        builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Abstra v1");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Abstra v1");
                });
                app.UseCors("Dev");
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
