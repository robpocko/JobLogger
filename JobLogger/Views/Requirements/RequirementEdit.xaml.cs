using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Requirements
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RequirementEdit : Page
    {
        private RequirementAPI requirement;

        public RequirementEdit()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboSources();
            SyncCombos();

            taskList.ItemsSource = requirement.tasks;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                requirement = e.Parameter as RequirementAPI;
                long ID;
                long.TryParse(requirement.id, out ID);
                if (ID > 0)
                {
                    TextRequirementID.IsReadOnly = true;
                }
            }
            else
            {
                requirement = new RequirementAPI();
            }
        }

        private void LoadComboSources()
        {
            foreach (RequirementStatus status in Enum.GetValues(typeof(RequirementStatus)))
            {
                if (status != RequirementStatus.All)
                {
                    ComboBoxStatus.Items.Add(status);
                }
            }
        }

        private void SyncCombos()
        {
            if (requirement.status > 0)
            {
                for (int i = 0; i < ComboBoxStatus.Items.Count; i++)
                {
                    if ((RequirementStatus)(ComboBoxStatus.Items[i]) == requirement.status)
                    {
                        ComboBoxStatus.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            requirement = await Requirement.Save(requirement);

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void TaskList_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;

            if (null == listView)
            {
                return;
            }

            TaskAPI task = listView.SelectedItem as TaskAPI;

            ((Frame)Parent).Navigate(
                typeof(Tasks.TaskEdit),
                task,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskAPI task = new TaskAPI { requirementID = long.Parse(requirement.id), isActive = true, isNew = true };

            ((Frame)Parent).Navigate(
                typeof(Tasks.TaskEdit),
                task,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
