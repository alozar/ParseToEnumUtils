using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ParseToEnumUtils;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<ParseToEnumService>();
                services.AddSingleton<ReadFilesService>();
            })
            .Build();

        string path = @"C:\Users\ZARKOVAV\Desktop\refer.cs123";
        host.Services
            .GetRequiredService<ParseToEnumService>()
            .ParseAndCreateAsync(path, "Refer");

        Console.ReadLine();
    }
}