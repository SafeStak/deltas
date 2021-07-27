using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace SafeStak.Deltas.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((hostContext, configurationBuilder) =>
                        {
                            configurationBuilder
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddEnvironmentVariables();
                        })
                        .ConfigureKestrel(serverOptions =>
                        {
                            //serverOptions.ListenUnixSocket("/tmp/kestrel.sock");
                            //serverOptions.ListenUnixSocket("/tmp/kestrel-test.sock",
                            //    listenOptions =>
                            //    {
                            //        listenOptions.UseHttps("testCert.pfx",
                            //            "testpassword");
                            //    });
                            //In the Nginx configuration file, set the server > location > proxy_pass entry to http://unix:/tmp/kestrel.sock:/;  
                            //Ensure that the socket is writeable by Nginx(for example, chmod go+w /tmp/kestrel.sock)).
                        })
                        .UseStartup<Startup>();
                });
    }
}
