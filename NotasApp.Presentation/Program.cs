using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotasApp.Domain.EstudianteDB;
using NotasApp.Domain.Interfaces;
using NotasApp.Infraestructure.Repository;
using NotasApp.Applications.Interfaces;
using NotasApp.Applications.Services;

namespace NotasApp.Presentation
{
    static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<PepitoSchoolContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("default"));
                });
            });

            var host = builder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            services.AddDbContext<PepitoSchoolContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("default"));
            });

            services.AddScoped<IPepitoSchoolContext, PepitoSchoolContext>();
            services.AddScoped<IEstudianteRepository, EFE_EstudianteRepository>();
            services.AddScoped<IEstudianteServices, EstudianteServices>();
            services.AddScoped<Form1>();

            using (var serviceScope = services.BuildServiceProvider())
            {
                var main = serviceScope.GetRequiredService<Form1>();
                Application.Run(main);
            }

        }
    }
}
