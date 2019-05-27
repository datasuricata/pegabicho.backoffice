using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using pegabicho.backoffice.Request.Base;
using pegabicho.backoffice.Services;
using pegabicho.backoffice.Services.Interfaces;
using System;

namespace pegabicho.backoffice.DI {
    public static class Bootstrap {
        public static void Configure(IServiceCollection services) {

            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(config => {
                // cookie settings
                config.Cookie.HttpOnly = true;
                config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // caminho de direcionamento de login
                config.LoginPath = "/Account/Login";
                // caminho de direcionamento para acessos negados
                config.AccessDeniedPath = "/Account/AccessDenied";
                config.SlidingExpiration = true;
            });

            services.AddScoped<IViewRender, ViewRender>();
            services.AddScoped<WebService>();
        }
    }
}
