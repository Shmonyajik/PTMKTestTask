
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PTMKTestTask.Entities;
using PTMKTestTask.Repository;
using PTMKTestTask.Services;
using Serilog;
using Serilog.Extensions.Hosting;

namespace PTMKTestTask
{
    public class Program
    {
       

        public static void Main(string[] args)
        {

            //var builder = new ConfigurationBuilder();
            //BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Log.txt")
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                services.AddDbContext<AppDbContext>((options) => options.UseSqlServer(@"Server=DESKTOP-7PL9DAP;Database=PTMKTestTaskDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));
                    services.AddScoped<IDynamicTableCreationService, DynamicTableCreationService>();
                    services.AddScoped<IBaseRepository<Employee>, EmployeeRepository>();
                    services.AddScoped<Application>();

                })
                .UseSerilog()
                .Build();

             var app = ActivatorUtilities.CreateInstance<Application>(host.Services);
             app.Run(args);
            Log.CloseAndFlush();
            #region old
            // var builder = Host.CreateDefaultBuilder()
            //     .ConfigureAppConfiguration((hostingContext, config) =>
            //     {
            //         config.SetBasePath(Directory.GetCurrentDirectory());
            //         config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //     })
            //     .ConfigureServices((context, services) => {

            //     services.AddDbContext<AppDbContext>((options) => options.UseSqlServer(@"Server=DESKTOP-7PL9DAP;Database=PTMKTestTaskDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"));
            //     services.AddScoped<IDynamicTableCreationService,DynamicTableCreationService>();
            //     services.AddScoped<IBaseRepository<Employee>, EmployeeRepository>();
            //     services.AddScoped<Application>();




            //}).Build();



            // var app = ActivatorUtilities.CreateInstance<Application>(builder.Services);
            // app.Run(args);
            #endregion

        }

        //static void BuildConfig(IConfigurationBuilder builder)
        //{
        //    builder.SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional:false, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT")??  "Production"}.json", optional: true)
        //        .AddEnvironmentVariables();
        //}
        




    }
}


