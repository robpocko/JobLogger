using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.TaskLogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskLogsList : Page
    {
        private TaskLogIncrementalLoad TaskLogList;

        public TaskLogsList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            TaskLogList = new TaskLogIncrementalLoad();

            await TaskLogList.LoadMoreItemsAsync();

            taskList.ItemsSource = TaskLogList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void taskList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            long taskLogId = (listView.SelectedItem as TaskLogsAPI).id;
            TaskLogAPI taskLog = await TaskLog.Get(taskLogId);

            ((Frame)Parent).Navigate(
                typeof(TaskLogs.TaskLogEdit),
                taskLog,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void ButtonAddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskLogAPI taskLog = new TaskLogAPI {
                                    logDateInternal = DateTime.Now.Date,
                                    startTime = DateTime.Now.ToString("HH:mm"),
                                    endTime = DateTime.Now.ToString("HH:mm")  };

            ((Frame)Parent).Navigate(
                typeof(TaskLogs.TaskLogEdit),
                taskLog,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
