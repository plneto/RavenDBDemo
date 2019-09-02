using System;

namespace RavenDbDemo.Api.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class SettingsExtensions
    {
        public static IServiceCollection ConfigureSettings<T>(
            this IServiceCollection services,
            IConfiguration configuration)
            where T : class
        {
            var section = configuration.GetSection(typeof(T).Name);

            return services.Configure<T>(section);
        }

        public static T GetSettings<T>(this IConfiguration configuration)
        {
            var section = configuration.GetSection(typeof(T).Name);

            var settings = (T)Activator.CreateInstance(typeof(T), new object[] { });

            section.Bind(settings);

            return settings;
        }
    }
}