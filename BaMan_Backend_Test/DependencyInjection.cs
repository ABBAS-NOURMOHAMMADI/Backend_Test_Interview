using Application.Interfaces;
using Application.MqService;
using MediatR;
using System.Reflection;

namespace BaMan_Backend_Test
{
    public static class DependencyInjection
    {
        public static void AddPresentationLayerServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<PublisherService>();
            services.AddScoped<SubscriberService>();

            services.AddMediatR(typeof(IApplicationDbContext).GetTypeInfo().Assembly);
        }
    }
}
