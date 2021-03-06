﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeRemoteFlights.Domain;
using AcmeRemoteFlights.Domain.Models;
using AcmeRemoteFlights.Domain.Repositories;
using AcmeRemoteFlights.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AcmeRemoteFlights
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FlightsDBContext>(opt =>
            opt.UseSqlite(Configuration.GetConnectionString("FlightsDB")));
            
            services.AddTransient<IRepository<Flight>, RepositoryBase<Flight>>();
            services.AddTransient<IRepository<BookedFlight>, RepositoryBase<BookedFlight>>();
            services.AddTransient<IBookingEngineService, BookingEngineService>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddMvc();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
