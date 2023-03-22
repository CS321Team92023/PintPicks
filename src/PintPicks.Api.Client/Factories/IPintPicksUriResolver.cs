
using JetBrains.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PintPicks.Api.Client.Abstractions
{
    /// <summary>
    /// An abstraction for URI resolution process.
    /// </summary>
    [PublicAPI]
    public interface IPintPicksUriResolver
    {
        /// <summary>
        /// Gets PintPicksAPI base URI.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to cancel this operation.</param>
        /// <returns>An asynchronous task which will get resolved to the PintPicksAPI URI.</returns>
        [NotNull, ItemCanBeNull]
        Task<Uri> GetPintPicksUriAsync(CancellationToken cancellationToken);
    }
}
