namespace EvenCart.Services.Installation
{
    public interface IInstallationService
    {
        void Install();

        void FillRequiredSeedData(string defaultUserEmail, string defaultUserPassword, string installDomain, string storeName);

        bool InstallSamplePackage(string packageFilePath);

        bool InstallSamplePackage(byte[] bytes);
    }
}