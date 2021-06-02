using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using CommandAPI.Data;
using Microsoft.Extensions.Configuration;//access configuration
using Microsoft.EntityFrameworkCore;//access DBcontext
using Npgsql; //so we can access NpgsqlConnectionStringBuilder class
using Newtonsoft.Json.Serialization; //for working with PATCH documents in ConfigureServices method


namespace CommandAPI
{
    public class Startup
    {
        public IConfiguration Configuration{get;}
        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Start setting connection to use database userID and password from our user secret file
            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];
            //End setting connection to use database userID and password from our user secret file

            //Register our DBContext (opt.UseNpgsql(..)) in the ConfigureServices method and pass it the connection stirng
            //Option 1: Use connection string info without using User Secret file
            //services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnection")));

            //Option 2: Use connection string info with userID and password from our user Secret file
            services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(builder.ConnectionString));
            
            //Use NewtonSoftJson package
            //Allows for the correct parsing of our Patch document.
            services.AddControllers().AddNewtonsoftJson(s =>{
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();});
            
            //Section 1. Registers services to enable the use of “Controllers” throughout our application.
            services.AddControllers();

            //DTO - Register auto mapper 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Applying Dependency Injection
            //Note: Require using CommandAPI.Data so we can access ICommandAPIRepo and MockCommandAPIRepo
            //services.AddScoped<ICommandAPIRepo,MockCommandAPIRepo>();
            //We comment out the hardcoded MockCommandAPIRepo class (above) to use data from the database below
            services.AddScoped<ICommandAPIRepo,SqlCommandAPIRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //Section 2. We “MapControllers” to our endpoints. This means we make use of 
                //the Controller services (registered in the ConfigureServices method) as endpoints 
                //in the 'Request Pipeline'.
                endpoints.MapControllers();
            });
        }
    }
}
