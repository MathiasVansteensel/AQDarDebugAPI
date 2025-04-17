using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace AQDarDebugAPI;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();
        //app.UseHttpsRedirection();

        app.Urls.Clear();
        app.Urls.Add("http://*:6969/");

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Static")),
            RequestPath = "/Static"
        });

        app.MapGet("/", () => Results.Content(File.ReadAllText("wwwroot/root.html"), "text/html"));
        app.MapGet("/kys", async () => await app.StopAsync());

        await app.RunAsync();
        Console.WriteLine("AHHHHHHHHHHHHHHHHHHHH");
    }

}
