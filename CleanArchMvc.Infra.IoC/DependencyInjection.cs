using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Application.Mappings;
using MediatR;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using CleanArchMvc.Domain.Account;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext)
                    .Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
                     options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfiler));
            var handlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(handlers));

            return services;
        }
    }
}
