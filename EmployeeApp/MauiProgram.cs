using EmployeeApp.Services;
using EmployeeApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace EmployeeApp
{
    public static class MauiProgram
    {
        //Creates a new Maui application
        public static MauiApp CreateMauiApp()
        {
            //build the application and return
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //Service
            builder.Services.AddSingleton<IEmployeeService, EmployeeSercice>();

            //views

            builder.Services.AddSingleton<EmployeesList>();
            builder.Services.AddTransient<AddEmployee>();

            //view models
            builder.Services.AddSingleton<EmployeesViewModel>();
            builder.Services.AddTransient<AddEmployeeViewModel>();



            return builder.Build();
        }
    }
}
