using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Features
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeaturesList : Page
    {
        private FeatureIncrementalLoad FeatureList;
        private static string               filterText     = string.Empty;
        private static RequirementStatus    filteredStatus = RequirementStatus.All;

        public FeaturesList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            RequirementStatus statusFilter = ComboBoxFeatureStatus.SelectedItem != null ?
                (RequirementStatus)(ComboBoxFeatureStatus.SelectedItem) :
                RequirementStatus.All;

            FeatureList = new FeatureIncrementalLoad(
                TextBoxFeatureTitleSearch.Text,
                statusFilter);

            await FeatureList.LoadMoreItemsAsync();

            featureList.ItemsSource = FeatureList;
        }

        private void ButtonAddFeature_Click(object sender, RoutedEventArgs e)
        {
            FeatureAPI feature = new FeatureAPI { status = RequirementStatus.Proposed, isNew = true };

            ((Frame)Parent).Navigate(
                typeof(Features.FeatureEdit),
                feature,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxFeatureTitleSearch.Text = filterText;

            await DoSearch();

            LoadComboBox();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void TextBoxFeatureTitleSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            filterText = TextBoxFeatureTitleSearch.Text;
            await DoSearch();
        }

        private async void featureList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            long featureId = (listView.SelectedItem as FeaturesAPI).id;
            FeatureAPI feature = await Feature.Get(featureId);

            ((Frame)Parent).Navigate(
                typeof(Features.FeatureEdit),
                feature,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void LoadComboBox()
        {
            foreach (RequirementStatus status in Enum.GetValues(typeof(RequirementStatus)))
            {
                ComboBoxFeatureStatus.Items.Add(status);
            }

            if (filteredStatus != RequirementStatus.All)
            {
                for (int i = 0; i < ComboBoxFeatureStatus.Items.Count; i++)
                {
                    if ((RequirementStatus)(ComboBoxFeatureStatus.Items[i]) == filteredStatus)
                    {
                        ComboBoxFeatureStatus.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void ComboBoxFeatureStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filteredStatus = (RequirementStatus)(ComboBoxFeatureStatus.SelectedItem);

            await DoSearch();
        }
    }
}
