﻿using Microsoft.AspNetCore.Authentication.Cookies;
using T1_PR2_Client.Services;
using Web_SOS_Code.Services;

namespace Web_SOS_Code
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            // 1.1 AuthService + HttpClient
            builder.Services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
            });

            builder.Services.AddHttpClient("AuthorizedClient", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
            })
                .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            builder.Services.AddScoped<GameService>();

            builder.Services.AddTransient<AuthenticationDelegatingHandler>();
            builder.Services.AddHttpClient("AuthorizedClient")
                   .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            // 1.3 Autenticación con cookies
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.SameSite = SameSiteMode.Strict;

                    // Evitar la redirección automática: devolver 401/403 en su lugar
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    };
                });

            // 1.4 Autorización (para [Authorize], políticas, etc.)
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}