using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiToken
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
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v2", new OpenApiInfo
        {
          Version = "v2",
          Title = "My Api",
          Description = "Citys & Users List \n Author: Tarık Davulcu",
          Contact = new OpenApiContact
          {
            Name = "Contact",
            Url = new Uri("https://www.tarikdavulcu.com/#contact"),
            Email = "info@tarikdavulcu.com",
          },
          License = new OpenApiLicense
          {
            Name = "License",
            Url = new Uri("https://tarikdavulcu.com/Blog/Detayi/Sql-Server-Identity-Seed-Reset"),
          },

        });
        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
      });
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                  jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                  {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]))
                  };
                });

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {



      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();


      }
      app.UseStaticFiles();
      app.UseSwagger(options =>
      {
        options.SerializeAsV2 = true;
      });
      app.UseSwaggerUI(c =>
      {


        c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
        c.InjectStylesheet("/swagger-custom/Style.css");
        c.InjectJavascript("/swagger-custom/Script.js", "text/javascript");
        c.RoutePrefix = string.Empty;

      });


      //Specify the MyCustomPage1.html as the default page
      //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
      //defaultFilesOptions.DefaultFileNames.Clear();
      //defaultFilesOptions.DefaultFileNames.Add("index.html");
      ////Setting the Default Files
      //app.UseDefaultFiles(defaultFilesOptions);
      //Adding Static Files Middleware to serve the static files


      app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
