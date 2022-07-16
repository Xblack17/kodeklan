using Coravel;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule.Interfaces;
using iTut.Coravel.Events;
using iTut.Coravel.Listeners;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace iTut.Coravel
{
    public static class CoravelExtensions
    {
        public static IServiceCollection AddCoravel(this IServiceCollection services)
        {
            services.AddQueue();
            services.AddEvents();
            services.AddScheduler();

            services.AddScoped<NewParentInviteListener>();
            services.AddScoped<ComplaintCreatedListener>();
            return services;
        }

        public static void UseCoravel(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;
            provider.UseScheduler(scheduler => { }).LogScheduledTaskProgress(provider.GetService<ILogger<IScheduler>>());

            provider.ConfigureQueue().OnError(e => Console.WriteLine(e)).LogQueuedTaskProgress(provider.GetService<ILogger<IQueue>>());

            var registration = provider.ConfigureEvents();
            registration.Register<NewParentInvite>().Subscribe<NewParentInviteListener>();
            registration.Register<ComplaintCreated>().Subscribe<ComplaintCreatedListener>();
        }
    }
}
