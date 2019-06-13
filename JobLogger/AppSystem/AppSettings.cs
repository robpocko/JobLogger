using Windows.Storage;

namespace JobLogger.AppSystem
{
    internal class AppSettings
    {
        public const string PASSWORD_LOCKER_RESOURCE_NAME = "au.com.robpocklington.app.joblogger";
        internal static ApplicationDataContainer localSettings;

        public static void Save()
        {
            //SaveCredentialsToLocker();
        }

        public static void Load()
        {
            //GetCredentialFromLocker();
        }

        public static ApplicationDataContainer GetApplicationDataContainer()
        {
            if (null == localSettings)
            {
                localSettings = ApplicationData.Current.LocalSettings;
            }

            return localSettings;
        }

        public static string ServerUrl
        {
            get
            {
                string url = (string)GetApplicationDataContainer().Values["ServerUrl"];

                if (string.IsNullOrEmpty(url))
                {
                    return string.Empty;
                }

                return url;
            }

            set
            {
                GetApplicationDataContainer().Values["ServerUrl"] = value;
            }
        }
    }
}
