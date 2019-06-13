using JobLogger.AppSystem;
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
            CodeBranchList = new CodeBranchIncrementalLoad();

            await CodeBranchList.LoadMoreItemsAsync();

            codeBranchList.ItemsSource = CodeBranchList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private void CodeBranchList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }
}
