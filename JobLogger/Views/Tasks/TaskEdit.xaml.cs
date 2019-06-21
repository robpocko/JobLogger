using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Tasks
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskEdit : Page
    {
        private TaskAPI task;

        public TaskEdit()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboSources();
            SyncCombos();

            TextBlockRequirementTitle.Visibility = task.requirement != null ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                task = e.Parameter as TaskAPI;
                long ID;
                long.TryParse(task.id, out ID);
                if (ID > 0)
                {
                    TextTaskID.IsReadOnly = true;
                }
            }
            else
            {
                task = new TaskAPI();
            }
        }

        private void LoadComboSources()
        {
            foreach (TaskType taskType in Enum.GetValues(typeof(TaskType)))
            {
                if (taskType != TaskType.All)
                {
                    ComboBoxTaskType.Items.Add(taskType);
                }              
            }
        }

        private void SyncCombos()
        {
            if (task.taskType > 0)
            {
                for (int i = 0; i < ComboBoxTaskType.Items.Count; i++)
                {
                    if ((TaskType)(ComboBoxTaskType.Items[i]) == task.taskType)
                    {
                        ComboBoxTaskType.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            task = await jlTask.Save(task);

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void TextBlockRequirementTitle_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            ((Frame)Parent).Navigate(
                typeof(Requirements.RequirementEdit),
                task.requirement,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private async void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            CommentDialog commentDialog = new CommentDialog();
            var dialogResult = await commentDialog.ShowAsync();

            if (dialogResult == ContentDialogResult.Primary)
            {
                string text;
                commentDialog.Comment.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out text);

                if (task.comments == null) task.comments = new List<TaskCommentAPI>();

                task.comments.Add(new TaskCommentAPI { comment = text });
            }
        }

        private void ShowCommentsButton_Click(object sender, RoutedEventArgs e)
        {
            ((Frame)Parent).Navigate(
                typeof(CommentsViewer),
                task.comments,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
