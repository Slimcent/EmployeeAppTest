﻿using EmployeeWeb.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.IntegrationTest
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<EmployeeContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<EmployeeContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
                try
                {
                    appContext.Database.EnsureCreated();
                    if (appContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                        appContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            });
        }
    }
}
