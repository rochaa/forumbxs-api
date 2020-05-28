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
using Posts.Domain.Commands;
using Posts.Domain.Handlers;
using Posts.Domain.Repositories;

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
            services.AddScoped<IRequestHandler<NewQuestionCommand, CommandResult>, PostHandler>();
            services.AddScoped<IRequestHandler<NewAnswerCommand, CommandResult>, PostHandler>();
            services.AddScoped<IRequestHandler<LikeQuestionCommand, CommandResult>, PostHandler>();
            services.AddScoped<IRequestHandler<LikeAnswerCommand, CommandResult>, PostHandler>();

            // Banco de dados (Em memória)
            services.AddDbContext<ForumBXSContext>(opt => opt.UseInMemoryDatabase(Settings.ForumBXSDatabaseName));
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();

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
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
