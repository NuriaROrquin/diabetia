using Refit;

namespace Diabetia.Infrastructure.Providers;

public class ImageRequest
{
    [AliasAs("imageId")]
    public int ImageId { get; set; }
}