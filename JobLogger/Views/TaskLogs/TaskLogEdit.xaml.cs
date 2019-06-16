using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.TaskLogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskLogEdit : Page
    {
        private TaskLogAPI  taskLog;

        public TaskLogEdit()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
  
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                taskLog = e.Parameter as TaskLogAPI;
                if (taskLog.id <= 0)
                {
                    TaskBugTitle.Visibility = Visibility.Collapsed;
                    TaskBugPicker.Visibility = Visibility.Visible;
                    await LoadTaskCombo();
                }
                else
                {
                    TaskBugTitle.Visibility = Visibility.Visible;
                    TaskBugPicker.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                taskLog = new TaskLogAPI();
            }
        }

        private void TaskBugPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                taskLog.taskID = (((ComboBox)sender).SelectedItem as TasksAPI).id;
            }
            else
            {
                taskLog.taskID = null;
            }
        }

        private async Task LoadTaskCombo()
        {
            TaskIncrementalLoad tasks = new TaskIncrementalLoad("", null, false);
            await tasks.LoadMoreItemsAsync();

            TaskBugPicker.ItemsSource = tasks;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            taskLog = await TaskLog.Save(taskLog);

            if (taskLog.id > 0)
            {
                TaskBugPicker.Visibility = Visibility.Collapsed;
                TaskBugTitle.Visibility = Visibility.Visible;
                TaskBugTitle.Text = taskLog?.task?.title ?? string.Empty;

                this.Bindings.Update();
            }
        }

        private void AddCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            CheckInAPI newCheckIn = new CheckInAPI {
                                            taskLog = taskLog,
                                            checkInTimeInternal = DateTime.Now,
                                            taskCheckIns = new List<TaskCheckInAPI> { new TaskCheckInAPI { taskID = taskLog.taskID.Value } },
                                            isNew = true };

            ((Frame)Parent).Navigate(
                typeof(CheckIns.CheckInEdit),
                newCheckIn,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
