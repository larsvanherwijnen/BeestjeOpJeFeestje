using BusinessLogic.Services.BookingValidationService;
using BusinessLogic.Services.PriceServices;
using Data;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Web;

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
        services.AddDbContext<BeestJeContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("BeestjeContext")));

        services.AddIdentity<AppUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
            .AddEntityFrameworkStores<BeestJeContext>();

        services.AddScoped<ICrudRepository<Customer>, CustomerRepository>();
        services.AddScoped<ICrudRepository<Animal>, AnimalRepository>();
        services.AddScoped<ICrudRepository<Booking>, BookingRepository>();
        services.AddSingleton<BookingValidationManager>();
        services.AddSingleton<PriceManager>();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CustomerOnly", policy => policy.RequireClaim("CustomerOnly"));
        });

        // services.AddDistributedMemoryCache();
        // services.AddSession(options =>
        // {
        //     options.Cookie.Name = ".Beestje.Session";
        //     options.IdleTimeout = TimeSpan.FromHours(24);
        //     options.Cookie.IsEssential = true;
        // });

        services.AddScoped<DataSeeder>();

        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BeestJeContext context, DataSeeder dataSeeder)
    {
        context.Database.Migrate();

        dataSeeder.SeedData();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        
        // app.UseSession();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}