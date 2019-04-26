namespace EvenCart.Infrastructure.Bundle
{
    public interface IBundleService
    {
        string GenerateCssBundle(string[] inputFiles);

        string GenerateJsBundle(string[] inputFiles);
    }
}