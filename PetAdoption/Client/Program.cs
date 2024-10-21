using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PetAdoption.Client;
using PetAdoption.Client.Services;

namespace PetAdoption.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<PetService>();
            builder.Services.AddScoped<OwnerService>();
            builder.Services.AddScoped<AdoptionService>();

            await builder.Build().RunAsync();
        }
    }
}
