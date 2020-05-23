using System;
using ForumBXS.Infra.Bus;
using ForumBXS.Infra.Contexts;
using ForumBXS.Infra.Repositories;
using ForumBXS.Shared;
using ForumBXS.Shared.Bus;
using ForumBXS.Shared.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Questions.Domain.Commands;
using Questions.Domain.Handlers;
using Questions.Domain.Repositories;

namespace ForumBXS.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Open API / Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ForumBXS",
                        Version = "v1",
                        Description = "Responsável pelo controle de perguntas e respostas do forum.",
                        Contact = new OpenApiContact
                        {
                            Name = "ForumBXS",
                            Url = new Uri("https://forumbxs.com.br/")
                        }
                    });
            });

            // Commands / Handlers
            services.AddScoped<IRequestHandler<NewQuestionCommand, CommandResult>, QuestionHandler>();

            // Banco de dados (Em memória)
            services.AddDbContext<ForumBXSContext>(opt => opt.UseInMemoryDatabase(Settings.ForumBXSDatabaseName));
            services.AddTransient<IQuestionRepository, QuestionRepository>();

            // Mediator (Em memória)
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, Bus>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ForumBXS V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
