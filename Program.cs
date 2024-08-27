using System.Net.Http.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    // Configurar CORS para permitir todas las solicitudes
                    services.AddCors(options =>
                    {
                        options.AddDefaultPolicy(builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
                        });
                    });
                });

                webBuilder.Configure(app =>
                {
                    app.UseRouting();

                    // Habilitar CORS
                    app.UseCors();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/character/{id}", async context =>
                        {
                            var id = context.Request.RouteValues["id"];
                            using var client = new HttpClient();
                            var character = await client.GetFromJsonAsync<Character>($"https://rickandmortyapi.com/api/character/{id}");

                            if (character == null)
                            {
                                context.Response.StatusCode = 404;
                                await context.Response.WriteAsJsonAsync(new { message = "Character not found" });
                            }
                            else
                            {
                                await context.Response.WriteAsJsonAsync(character);
                            }
                        });

                        endpoints.MapGet("/", async context =>
                        {
                            await context.Response.WriteAsync("Bienvenido a la API de RICK Y MORTY");
                        });
                    });
                });
            })
            .Build();

        host.Run();
    }
}
