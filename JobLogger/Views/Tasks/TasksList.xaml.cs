using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Tasks
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TasksList : Page
    {
        private TaskIncrementalLoad TaskList;
        private static string               filterText          = string.Empty;
        private static RequirementStatus    filteredStatus      = RequirementStatus.All;
        private static bool                 filteredIsActive    = false;

        public TasksList()
        {
            this.InitializeComponent();
        }

        private async Task DoSearch()
        {
            TaskType? taskTypeFilter = ComboBoxTaskType.SelectedItem != null ?
                (TaskType)(ComboBoxTaskType.SelectedItem) :
                (TaskType?)null;

            TaskList = new TaskIncrementalLoad(
                TextBoxTaskTitleSearch.Text,
                taskTypeFilter,
                CheckBoxShowInactive.IsChecked.Value);

            await TaskList.LoadMoreItemsAsync();

            taskList.ItemsSource = TaskList;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxTaskTitleSearch.Text = filterText;
            CheckBoxShowInactive.IsChecked = filteredIsActive;

            await DoSearch();

            LoadComboBox();
        }

        private async void TextBoxRequirementTitleSearch_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void ComboBoxRequirementStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await DoSearch();
        }

        private async void CheckBoxShowHidden_Unchecked(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void CheckBoxShowInactiven_Checked(object sender, RoutedEventArgs e)
        {
            await DoSearch();
        }

        private async void taskList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            filteredStatus      = (RequirementStatus)(ComboBoxTaskType.SelectedItem ?? RequirementStatus.All);
            filteredIsActive    = CheckBoxShowInactive.IsChecked.Value;
            filterText          = TextBoxTaskTitleSearch.Text;

            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            long taskId = (listView.SelectedItem as TasksAPI).id;
            TaskAPI task = await jlTask.Get(taskId);

            ((Frame)Parent).Navigate(
                typeof(Tasks.TaskEdit),
                task,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void LoadComboBox()
        {
            foreach (TaskType taskType in Enum.GetValues(typeof(TaskType)))
            {
                ComboBoxTaskType.Items.Add(taskType);
            }

            if (filteredStatus != RequirementStatus.All)
            {
                for (int i = 0; i < ComboBoxTaskType.Items.Count; i++)
                {
                    if ((RequirementStatus)(ComboBoxTaskType.Items[i]) == filteredStatus)
                    {
                        ComboBoxTaskType.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void ButtonAddTask_Click(object sender, RoutedEventArgs e)
        {
            filteredStatus      = (RequirementStatus)(ComboBoxTaskType.SelectedItem ?? RequirementStatus.All);
            filteredIsActive    = CheckBoxShowInactive.IsChecked.Value;
            filterText          = TextBoxTaskTitleSearch.Text;

            TaskAPI task = new TaskAPI { isActive = true, isNew = true };

            ((Frame)Parent).Navigate(
                typeof(Tasks.TaskEdit),
                task,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
