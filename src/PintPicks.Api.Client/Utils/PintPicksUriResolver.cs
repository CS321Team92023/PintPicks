using PintPicks.Api.Client.Abstractions;
using JetBrains.Annotations;
using PintPicks.Api.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PintPicks.Api.Client.Utils
{
    /// <summary>
    /// Resolves uris
    /// </summary>
    public class PintPicksUriResolver : IPintPicksUriResolver
    {

        #region Fields

        [NotNull] private readonly Uri _uri;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes <see cref="StaticMLPUriResolver"/> instance.
        /// </summary>
        /// <param name="uri">Static URI to return.</param>
        public PintPicksUriResolver([NotNull] Uri uri)
        {
            Assert.NotNull(uri, nameof(uri));
            _uri = uri;
        }

        #endregion Constructors

        #region Public Methods

        /// <inheritdoc />
        public Task<Uri> GetPintPicksUriAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_uri);
        }

        #endregion Public Methods
    }
}
