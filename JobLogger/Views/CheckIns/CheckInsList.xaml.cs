using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.CheckIns
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CheckInsList : Page
    {
        CheckInIncrementalLoad CheckInList;

        public CheckInsList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            long codeBranchFilterId = ComboBoxCodeBranch.SelectedItem != null ?
                ((CodeBranchesAPI)(ComboBoxCodeBranch.SelectedItem)).id :
                0;

            CheckInList = new CheckInIncrementalLoad(TextBoxCheckInCommentSearch.Text, codeBranchFilterId != 0 ? codeBranchFilterId : (long?)null);

            await CheckInList.LoadMoreItemsAsync();

            checkInList.ItemsSource = CheckInList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DoSearch();
            await LoadCombo();
        }

        private async void CheckInList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            long checkInId = (listView.SelectedItem as CheckInsAPI).id;
            CheckInAPI checkIn = await CheckIn.Get(checkInId);

            ((Frame)Parent).Navigate(
                typeof(CheckIns.CheckInEdit),
                checkIn,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private async void TextBoxCheckInCommentSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void ComboBoxCodeBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await DoSearch();
        }

        private async Task LoadCombo()
        {
            CodeBranchIncrementalLoad CodeBranchList = new CodeBranchIncrementalLoad();

            await CodeBranchList.LoadMoreItemsAsync();

            ComboBoxCodeBranch.Items.Add(new CodeBranchesAPI { id = 0, name = "All" });

            foreach (var codebranch in CodeBranchList)
            {
                ComboBoxCodeBranch.Items.Add(codebranch);
            }
        }
    }
}
