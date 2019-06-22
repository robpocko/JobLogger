using JobLogger.AppSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class CommentsViewer : Page
    {
        private List<CommentAPI> comments;
        private CommentAPI currentComment;
        private int currentCommentIndex;

        public CommentsViewer()
        {

            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                comments = new List<CommentAPI>();

                if (e.Parameter as List<TaskCommentAPI> != null)
                {
                    foreach (CommentAPI comment in e.Parameter as List<TaskCommentAPI>)
                    {
                        comments.Add(comment);
                    }
                }

                currentComment = comments[0];
                currentCommentIndex = 0;

                CommentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, currentComment.comment);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentCommentIndex < comments.Count() - 1)
            {
                currentCommentIndex++;
                currentComment = comments[currentCommentIndex];

                CommentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, currentComment.comment);
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentCommentIndex > 0)
            {
                currentCommentIndex--;
                currentComment = comments[currentCommentIndex];

                CommentText.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, currentComment.comment);
            }
        }
    }
}
