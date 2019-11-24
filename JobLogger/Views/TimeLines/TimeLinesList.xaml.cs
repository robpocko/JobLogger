using JobLogger.AppSystem;
using JobLogger.AppSystem.UI;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.TimeLines
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeLinesList : Page
    {
        private TimeLineIncrementalLoad TimeLineList;

        public TimeLinesList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            TimeLineList = new TimeLineIncrementalLoad();

            await TimeLineList.LoadMoreItemsAsync();

            timeLineList.ItemsSource = TimeLineList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private void timeLineList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxTimeLineTitleSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {

        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxShowInactive_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxShowInactive_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
