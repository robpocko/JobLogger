using Windows.UI.Text;
using Windows.UI.Xaml.Controls;

namespace JobLogger.Views
{
    public sealed partial class CommentDialog : ContentDialog
    {
        public ITextDocument Comment { get; private set; }

        public CommentDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Comment = CommentText.TextDocument;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
    }
}
