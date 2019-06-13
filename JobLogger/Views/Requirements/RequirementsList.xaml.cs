using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Requirements
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RequirementsList : Page
    {
        private RequirementIncrementalLoad  RequirementList;

        private static string               filterText      = string.Empty;
        private static RequirementStatus    filteredStatus  = RequirementStatus.All;

        public RequirementsList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            RequirementStatus statusFilter = ComboBoxRequirementStatus.SelectedItem != null ?
                (RequirementStatus)(ComboBoxRequirementStatus.SelectedItem) :
                RequirementStatus.All;

            RequirementList = new RequirementIncrementalLoad(
                TextBoxRequirementTitleSearch.Text,
                statusFilter);

            await RequirementList.LoadMoreItemsAsync();

            requirementList.ItemsSource = RequirementList;
        }

        private void ButtonAddRequirement_Click(object sender, RoutedEventArgs e)
        {
            RequirementAPI requirement = new RequirementAPI { status = RequirementStatus.Proposed, isNew = true };

            ((Frame)Parent).Navigate(
                typeof(Requirements.RequirementEdit),
                requirement,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxRequirementTitleSearch.Text = filterText;

            await DoSearch();

            LoadComboBox();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void TextBoxRequirementTitleSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            filterText = TextBoxRequirementTitleSearch.Text;

            await DoSearch();
        }

        private async void requirementList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            long requirementId = (listView.SelectedItem as RequirementsAPI).id;
            RequirementAPI requirement = await Requirement.Get(requirementId);

            ((Frame)Parent).Navigate(
                typeof(Requirements.RequirementEdit),
                requirement,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void LoadComboBox()
        {
            foreach (RequirementStatus status in Enum.GetValues(typeof(RequirementStatus)))
            {
                ComboBoxRequirementStatus.Items.Add(status);
            }

            if (filteredStatus != RequirementStatus.All)
            {
                for (int i = 0; i < ComboBoxRequirementStatus.Items.Count; i++)
                {
                    if ((RequirementStatus)(ComboBoxRequirementStatus.Items[i]) == filteredStatus)
                    {
                        ComboBoxRequirementStatus.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void ComboBoxRequirementStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filteredStatus = (RequirementStatus)(ComboBoxRequirementStatus.SelectedItem);

            await DoSearch();
        }
    }
}
