

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using PintPicks.Api.Client.Abstractions;
using PintPicks.Api.Client.Utils;
using PintPicks.Api.Client.Factories;

namespace PintPicks.Api.Client
{
    /// <summary>
    /// Extensions methods to add the job factories to <see cref="IServiceCollection" />.
    /// </summary>
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds PintPicks API discovery services based on a static API URI to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance to enhance with the PintPicks discovery services.</param>
        /// <param name="uri">PintPicks API URI.</param>
        /// <returns>The given <see cref="IServiceCollection"/> instance to support calls chaining.</returns>
        public static IServiceCollection AddPintPicksUri(this IServiceCollection services, [NotNull] Uri uri)
        {
            return services.AddSingleton<IPintPicksUriResolver>(new PintPicksUriResolver(uri));
        }

        /// <summary>
        /// Adds PintPicks API Client services to the <see cref="IServiceCollection"/> and configured a related <see cref="HttpClient"/> to use. 
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance to enhance with the Client services.</param>
        /// <param name="uri">PintPicks API URI.</param>
        /// <returns>The given <see cref="IServiceCollection"/> instance to support calls chaining.</returns>
        public static IServiceCollection AddPintPicksClient(this IServiceCollection services, Uri uri = null)
        {
            if (uri != null)
            {
                services.AddPintPicksUri(uri);
            }

            return services.AddHttpClient(PintPicksClientFactory.HttpClientName)
                .Services
                .AddSingleton<IPintPicksClientFactory, PintPicksClientFactory>();
        }
    }
}
