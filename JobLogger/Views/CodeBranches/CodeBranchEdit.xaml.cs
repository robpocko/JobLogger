using JobLogger.AppSystem.DataAccess;
using JobLogger.AppSystem.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.CodeBranches
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeBranchEdit : Page
    {
        private CodeBranchAPI codeBranch;

        public CodeBranchEdit()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            codeBranch = await CodeBranch.Save(codeBranch);

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                codeBranch = e.Parameter as CodeBranchAPI;
            }
            else
            {
                codeBranch = new CodeBranchAPI();
            }
        }
    }
}
