using PintPicks.Api.Client.Abstractions;
using PintPicks.Api.Client.Clients;
using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PintPicks.Api.Client.Factories
{

    /// <inheritdoc cref="IPintPicksClientFactory" />
    /// <summary>
    /// Creates and initializes PintPicks API client instances based on Microsoft's <see cref="IHttpClientFactory"/> service.<br/>
    /// This factory class is a recommended approach to ensure that underlying <see cref="HttpClient"/> instances are used in a proper way.<br/>
    /// </summary>
    [PublicAPI]
    public sealed class PintPicksClientFactory : IPintPicksClientFactory
    {
        #region Constants

        internal const string HttpClientName = "PintPicks";

        #endregion Constants

        #region Fields

        [NotNull] private readonly IHttpClientFactory _httpClientFactory;
        [NotNull] private readonly IPintPicksUriResolver _uriResolver;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialize <see cref="PintPicksClientFactory"/> instance.
        /// </summary>
        /// <param name="httpClientFactory"><see cref="IHttpClientFactory"/> instance to create underlying <see cref="HttpClient"/> instances.</param>
        /// <param name="uriResolver"><see cref="IMLPUriResolver"/> instance to resolve PintPicks API base URI.</param>
        public PintPicksClientFactory([NotNull] IHttpClientFactory httpClientFactory, [NotNull] IPintPicksUriResolver uriResolver)
        {
            Assert.NotNull(httpClientFactory, nameof(httpClientFactory));
            Assert.NotNull(uriResolver, nameof(uriResolver));

            _httpClientFactory = httpClientFactory;
            _uriResolver = uriResolver;
        }

        #endregion Constructors

        #region Public Methods

        /// <inheritdoc />
        public async Task<TClientType> CreateAsync<TClientType>(CancellationToken cancellationToken = default) where TClientType : PintPicksClient
        {
            var uri = await _uriResolver.GetPintPicksUriAsync(cancellationToken).ConfigureAwait(false);
            var client = _httpClientFactory.CreateClient(HttpClientName);
            client.BaseAddress = uri;
            object[] constParams = { client };
            var obj = Activator.CreateInstance(typeof(TClientType), constParams);
            return (TClientType)obj;
        }

        #endregion Public Methods
    }
}

