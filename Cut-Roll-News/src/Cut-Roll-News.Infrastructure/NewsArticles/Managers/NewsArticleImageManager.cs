namespace Cut_Roll_News.Infrastructure.NewsArticles.Managers;

using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Cut_Roll_News.Core.Blob.BlobOptions;
using Cut_Roll_News.Core.Blob.Managers;
using Cut_Roll_News.Core.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class NewsArticleImageManager : BaseBlobImageManager<Guid>
{
    IMessageBrokerService _messageBrokerService;
    private readonly string _defaultAvatarUrl;

    public NewsArticleImageManager(BlobServiceClient blobServiceClient,
    IMessageBrokerService messageBrokerService, IOptions<BlobOptions> blobOptions) : base(blobServiceClient, blobOptions.Value.ContainerName, blobOptions.Value.Directory)
    {
        _messageBrokerService = messageBrokerService;
        _defaultAvatarUrl = GetDefaultImageUrl();
    }

    public override async Task<string> DeleteImageAsync(string path, Guid id)
    {
        if (!string.IsNullOrEmpty(path) && !path.Equals(_defaultAvatarUrl, StringComparison.OrdinalIgnoreCase))
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobUri = new Uri(path).AbsolutePath.TrimStart('/');
            var blobName = Path.GetFileName(blobUri);
            var blobClient = containerClient.GetBlobClient($"{_directory}/{blobName}");

            await blobClient.DeleteIfExistsAsync();
        }

        return _defaultAvatarUrl;
    }

    public async override Task<string> SetImageAsync(Guid id, IFormFile? logo)
    {
        if (logo == null || logo.Length == 0)
        {
            return _defaultAvatarUrl;
        }

        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobName = $"{id}{Path.GetExtension(logo.FileName)}";
        var blobClient = containerClient.GetBlobClient($"{_directory}/{blobName}");

        using (var stream = logo.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = logo.ContentType });
        }

        var avatarUrl = blobClient.Uri.ToString();

        return avatarUrl;
    }
}
