
/* Cambio no fusionado mediante combinaci�n del proyecto 'ARWNI2S.Serialization (net9.0)'
Antes:
using Microsoft.Extensions.DependencyInjection;
Despu�s:
using ARWNI2S;
using ARWNI2S.Serialization;
using ARWNI2S.Serialization.Utilities;
using ARWNI2S.Serialization.Utilities;
using ARWNI2S.Serialization.Utilities.Internal;
using Microsoft.Extensions.DependencyInjection;
*/
using Microsoft.Extensions.DependencyInjection;

namespace ARWNI2S.Serialization.Utilities
{
    /// <summary>
    /// Extension methods for configuring dependency injection.
    /// </summary>
    public static class InternalServiceCollectionExtensions
    {
        /// <summary>
        /// Registers an existing registration of <typeparamref name="TImplementation"/> as a provider of service type <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">The service type being provided.</typeparam>
        /// <typeparam name="TImplementation">The implementation of <typeparamref name="TService"/>.</typeparam>
        /// <param name="services">The service collection.</param>
        public static void AddFromExisting<TService, TImplementation>(this IServiceCollection services) where TImplementation : TService
        {
            services.AddFromExisting(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Registers an existing registration of <paramref name="implementation"/> as a provider of service type <paramref name="service"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="service">The service type being provided.</param>
        /// <param name="implementation">The implementation of <paramref name="service"/>.</param>
        public static void AddFromExisting(this IServiceCollection services, Type service, Type implementation)
        {
            ServiceDescriptor registration = null;
            foreach (var descriptor in services)
            {
                if (descriptor.ServiceType == implementation)
                {
                    registration = descriptor;
                    break;
                }
            }

            if (registration is null)
            {
                throw new ArgumentNullException(nameof(implementation), $"Unable to find previously registered ServiceType of '{implementation.FullName}'");
            }

            var newRegistration = new ServiceDescriptor(
                service,
                sp => sp.GetRequiredService(implementation),
                registration.Lifetime);
            services.Add(newRegistration);
        }
    }
}