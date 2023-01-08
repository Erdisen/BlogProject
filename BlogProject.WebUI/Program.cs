using BlogProject.Core.Service;
using BlogProject.Entities.Context;
using BlogProject.Service.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogProject.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddDbContext<BlogProjectContext>(options => options.UseSqlServer("Server=DESKTOP-JH0OB08; Database=BlogProject; uid=sa; pwd=1; "));

            //.Net Core MVC'de tamamen Dependency Injection yapýsýyla çalýþýyoruz.ICoreService interface'inin BaseService ile olan gevþek baðýmlýlýðýný tanýmlýyoruz. Nerede ICoreService çaðýrýlýrsa, onun yerine BaseService gönderilecektir.
            builder.Services.AddScoped(typeof(ICoreService<>), typeof(BaseService<>));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = "Account/Login"; // Yetki istenilen sayfalara girmek istediðimizde bizi yönlendireceði sayfayý belirliyoruz.
                   
                });
             


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}