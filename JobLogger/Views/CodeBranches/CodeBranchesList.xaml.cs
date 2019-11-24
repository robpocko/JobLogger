using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.CodeBranches
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeBranchesList : Page
    {
        private CodeBranchIncrementalLoad CodeBranchList;

        public CodeBranchesList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            CodeBranchList = new CodeBranchIncrementalLoad(TextBoxCodeBranchSearch.Text, CheckBoxShowInactive.IsChecked.Value);

            await CodeBranchList.LoadMoreItemsAsync();

            codeBranchList.ItemsSource = CodeBranchList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void CodeBranchList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            long codeBranchId = (listView.SelectedItem as CodeBranchesAPI).id;
            CodeBranchAPI codeBranch = await CodeBranch.Get(codeBranchId);

            ((Frame)Parent).Navigate(
                typeof(CodeBranches.CodeBranchEdit),
                codeBranch,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void ButtonAddRequirement_Click(object sender, RoutedEventArgs e)
        {
            CodeBranchAPI feature = new CodeBranchAPI { isActive = true };

            ((Frame)Parent).Navigate(
                typeof(CodeBranches.CodeBranchEdit),
                feature,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private async void TextBoxCodeBranchSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void CheckBoxShowInactive_Checked(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void CheckBoxShowInactive_Unchecked(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }
    }
}
