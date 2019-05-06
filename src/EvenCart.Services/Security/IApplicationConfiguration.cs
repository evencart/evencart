namespace EvenCart.Services.Security
{
    public interface IApplicationConfiguration
    {
        string GetSetting(string settingName);

        void SetSetting(string settingName, string value);
    }
}