
using JetBrains.Annotations;
using Newtonsoft.Json;
using PintPicks.Api.Contract;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PintPicks.Api.Client.Clients
{
    public class MenuClient : PintPicksClient
    {

        #region Constructors
        /// <summary>
        /// Initializes <see cref="MenuClient" /> instance.
        /// </summary>
        /// <param name="httpClient"><see cref="HttpClient" /> instance to use for HTTP communication.</param>
        public MenuClient([NotNull] HttpClient httpClient) : base(httpClient) { }
        #endregion Constructors

        /// <summary>
        /// Obtains a list of <see cref="Pint" /> objects that are analyzed from a given image file
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <param name="authToken">The authorization token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of <see cref="Pint" /></returns>
        public async Task<List<Pint>> GetPintsFromMenuImage(
            string filePath,
            string authToken = null,
            CancellationToken cancellationToken = default)
        {
            var resourceKey = await UploadFileToS3(filePath, authToken, cancellationToken).ConfigureAwait(false);
            var pints = await GetPintsFromMenuResource(resourceKey, authToken, cancellationToken).ConfigureAwait(false);
            return pints;
        }

        /// <summary>
        /// Obtains a list of <see cref="Pint" /> objects that are analyzed from a given image file
        /// </summary>
        /// <param name="fileStream">The stream of the file</param>
        /// <param name="fileExtension">The extension of the file</param>
        /// <param name="authToken">The authorization token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of <see cref="Pint" /></returns>
        public async Task<List<Pint>> GetPintsFromMenuImage(
            Stream fileStream,
            string fileExtension,
            string authToken = null,
            CancellationToken cancellationToken = default)
        {
            var resourceKey = await UploadFileToS3(fileStream, fileExtension, authToken, cancellationToken).ConfigureAwait(false);
            var pints = await GetPintsFromMenuResource(resourceKey, authToken, cancellationToken).ConfigureAwait(false);
            return pints;
        }

        /// <summary>
        /// Uploads a file to the S3 bucket assigned for pintpicks using a temporary signed PUT url
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <param name="authToken">The authorization token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The S3 resource key of the file </returns>
        public async Task<string> UploadFileToS3(
            string filePath,
            string authToken = null,
            CancellationToken cancellationToken = default)
        {
            using var readStream = File.OpenRead(filePath);
            return await UploadFileToS3(readStream, Path.GetExtension(filePath), authToken, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Uploads a file to the S3 bucket assigned for pintpicks using a temporary signed PUT url
        /// </summary>
        /// <param name="fileStream">The stream of the file</param>
        /// <param name="fileExtension">The extension of the file</param>
        /// <param name="authToken">The authorization token</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The S3 resource key of the file </returns>
        public async Task<string> UploadFileToS3(
            Stream fileStream,
            string fileExtension,
            string authToken = null,
            CancellationToken cancellationToken = default)
        {
            var uploadUrlModel = await GetUploadUrl(fileExtension, authToken, cancellationToken).ConfigureAwait(false);
            var uploadUrl = uploadUrlModel?.Url.Replace("\"", string.Empty);

            using var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            var response = await Client.PutAsync(new Uri(uploadUrl), fileContent).ConfigureAwait(false);
            await EnsureSuccessfulRequest(response).ConfigureAwait(false);

            return uploadUrlModel?.Key;
        }

        /// <summary>
        /// Obtains a signed upload S3 PUT url that can be used to upload a resource to the pintpicks bucket
        /// </summary>
        /// <param name="extension">The extension of the file to upload</param>
        /// <returns>
        /// An object containing the upload url and the resource key.
        /// </returns>
        private async Task<UploadUrlReturnModel> GetUploadUrl(
            string extension,
            string authToken = null,
            CancellationToken cancellationToken = default)
        {
            string apiURL = $"/development/menu/uploads?extension={extension}";
            return await GetAsync<UploadUrlReturnModel>(apiURL, authToken, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Returns the list of pints analyzed from the requested S3 resource.
        /// </summary>
        /// <param name="resource">The S3 resource.</param>
        /// <returns>
        /// Returns the list of pints analyzed from the requested S3 resource.
        /// </returns>
        public async Task<List<Pint>> GetPintsFromMenuResource(
            string resource,
            string authToken = null,
            CancellationToken cancellationToken = default)
        {
            string apiURL = $"/development/menu?resource={resource}";
            return await GetAsync<List<Pint>>(apiURL, authToken, cancellationToken).ConfigureAwait(false);
        }
    }
}
