using PintPicks.Api.Client.Clients;
using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PintPicks.Api.Client.Abstractions
{
    /// <summary>
    /// Creates and initializes PintPicks API client instances (<see cref="PintPicksClient" />).
    /// </summary>
    /// <remarks>This interface was introduced to allow mocking in unit tests.</remarks>
    public interface IPintPicksClientFactory
    {
        /// <summary>
        /// Creates and initializes <see cref="PintPicksClient"/> instance.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to cancel this operation.</param>
        /// <returns>An asynchronous task which will get resolved to <see cref="PintPicksClient"/> instance.</returns>
        /// <exception cref="InvalidOperationException">when PintPicks API URI cannot be resolved.</exception>
        [NotNull, ItemNotNull]
        Task<TClientType> CreateAsync<TClientType>(CancellationToken cancellationToken = default) where TClientType : PintPicksClient;
    }
}
