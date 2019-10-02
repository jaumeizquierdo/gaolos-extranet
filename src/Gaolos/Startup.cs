using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Gaolos
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
            // Add framework services.
            services.AddMvc();

            services.AddDistributedMemoryCache();

            //services.AddSession();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });


            //*********************************************************************************************
            var assembly1 = typeof(MenuGaolosLibrary.MenuGaolosComponent).GetTypeInfo().Assembly;
            var embeddedFileProvider1 = new EmbeddedFileProvider(assembly1, "MenuGaolosLibrary");

            var assembly2 = typeof(MenuUsuarioLibrary.MenuUsuarioComponent).GetTypeInfo().Assembly;
            var embeddedFileProvider2 = new EmbeddedFileProvider(assembly2, "MenuUsuarioLibrary");

            var assembly3 = typeof(MenuNotificacionesLibrary.MenuNotificacionesComponent).GetTypeInfo().Assembly;
            var embeddedFileProvider3 = new EmbeddedFileProvider(assembly3, "MenuNotificacionesLibrary");

            var assembly4 = typeof(MenuFooterLibrary.MenuFooterComponent).GetTypeInfo().Assembly;
            var embeddedFileProvider4 = new EmbeddedFileProvider(assembly4, "MenuFooterLibrary");

            var assembly5 = typeof(IdiomasLibrary.IdiomasComponent).GetTypeInfo().Assembly;
            var embeddedFileProvider5 = new EmbeddedFileProvider(assembly5, "IdiomasLibrary");

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(embeddedFileProvider1);
                options.FileProviders.Add(embeddedFileProvider2);
                options.FileProviders.Add(embeddedFileProvider3);
                options.FileProviders.Add(embeddedFileProvider4);
                options.FileProviders.Add(embeddedFileProvider5);
            });


            //*********************************************************************************************

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSession();

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationScheme = "MyCookieMiddlewareInstance",
            //    LoginPath = new PathString("/Account/Unauthorized/"),
            //    AccessDeniedPath = new PathString("/Account/Forbidden/"),
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}"); //template: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
