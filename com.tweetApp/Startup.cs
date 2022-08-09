using com.tweetApp.DAO;
using com.tweetApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.tweetApp
{
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "com.tweetapp", Version = "v1" });
            });
            services.Configure<TweetAppDatabaseSettings>(Configuration.GetSection(nameof(TweetAppDatabaseSettings)));
            services.AddSingleton<ITweetAppDatabaseSettings>(ui =>
                ui.GetRequiredService<IOptions<TweetAppDatabaseSettings>>().Value);
            services.AddSingleton<IMongoClient>(mc =>
                new MongoClient(Configuration.GetValue<string>("TweetAppDatabaseSettings:ConnectionString")));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITweetService, TweetService>();
            services.AddScoped<IReplyService, ReplyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "com.tweetapp v1"));
            }

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
