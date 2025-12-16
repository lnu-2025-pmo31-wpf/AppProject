using BLL.Interfaces;
using BLL.Services;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ViewModels;
using Presentation.Views;
using System.Windows;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>();
        services.AddScoped<ITransactionService, TransactionService>();

        services.AddTransient<TransactionsViewModel>();
        services.AddTransient<TransactionsWindow>();

        Services = services.BuildServiceProvider();

        Services.GetRequiredService<TransactionsWindow>().Show();
    }
}
