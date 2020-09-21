using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Xunit;
using XYZ.Starter.Data;

namespace XYZ.Starter.Integration.Tests
{
    /// <summary>
    /// This class is used to set up the test service and client
    /// </summary>
    /// <typeparam name="TStatup"></typeparam>
    public class TestFixture<TStatup> : IDisposable
    {
        /// <summary>
        /// Get the application project path where the startup assembly lives
        /// </summary>
        /// <param name="projectRelativePath">The path relative to the project containing the assembly that will be used in testing</param>
        /// <param name="startupAssembly">The assembly that contains the Startup class used in normal service startup</param>
        /// <returns></returns>
        string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;

            var applicationBaseBath = AppContext.BaseDirectory;

            var directoryInfo = new DirectoryInfo(applicationBaseBath);

            do
            {
                directoryInfo = directoryInfo.Parent;
                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                {
                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                }
            } while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using application root {applicationBaseBath}");
        }

        /// <summary>
        /// The temporary test server that will be used to host the controllers
        /// </summary>
        private TestServer _server;

        /// <summary>
        /// The client used to send information to the service host server
        /// </summary>
        public HttpClient HttpClient { get; }

        public TestFixture() : this(Path.Combine(""))
        { }

        protected TestFixture(string relativeTargetProjectParentDirectory)
        {
            var startupAssembly = typeof(TStatup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(relativeTargetProjectParentDirectory, startupAssembly);

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json");


            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(TStatup));

            //create test instance of the server
            _server = new TestServer(webHostBuilder);

            //configure client
            HttpClient = _server.CreateClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5005");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        /// <summary>
        /// Initialize the services so that it matches the services used in the main API project
        /// </summary>
        /// <param name="services"></param>
        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAsembly = typeof(TStatup).GetTypeInfo().Assembly;
            var manager = new ApplicationPartManager
            {
                ApplicationParts = {
                    new AssemblyPart(startupAsembly)
                },
                FeatureProviders = {
                    new ControllerFeatureProvider()
                }
            };
            services.AddSingleton(manager);
        }

        /// <summary>
        /// Dispose the Client and the Server
        /// </summary>
        public void Dispose()
        {
            HttpClient.Dispose();
            _server.Dispose();
            _ctx.Dispose();
        }

        AppDbContext _ctx = null;
        public void SeedDataToContext()
        {
            if (_ctx == null)
            {
                _ctx = _server.Services.GetService<AppDbContext>();
                if (_ctx != null)
                    _ctx.SeedAppDbContext();
            }
        }
    }
}
