using JobLogger.AppSystem;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();

            LoadSettings();
        }

        private void LoadSettings()
        {
            UrlText.Text = AppSettings.ServerUrl;
            BackupLocationText.Text = AppSettings.BackupLocation;
        }

        private void SaveUrlButton_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.ServerUrl = UrlText.Text.Trim();
            AppSettings.BackupLocation = BackupLocationText.Text.Trim();
        }
    }
}
