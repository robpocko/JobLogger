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
    public sealed partial class CheckInEdit : Page
    {
        private CheckInAPI checkIn;

        public CheckInEdit()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadComboSources();
            SyncCombos();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                checkIn = e.Parameter as CheckInAPI;
            }
            else
            {
                checkIn = new CheckInAPI();
            }
        }

        private async Task LoadComboSources()
        {
            CodeBranchIncrementalLoad CodeBranchList = new CodeBranchIncrementalLoad();

            await CodeBranchList.LoadMoreItemsAsync();

            CodeBranchPicker.ItemsSource = CodeBranchList;
        }

        private void SyncCombos()
        {
            if (checkIn.codeBranchID > 0)
            {
                for (int i = 0; i < CodeBranchPicker.Items.Count; i++)
                {
                    if (((CodeBranchesAPI)(CodeBranchPicker.Items[i])).id == checkIn.codeBranchID)
                    {
                        CodeBranchPicker.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void CodeBranchPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                checkIn.codeBranchID = (comboBox.SelectedItem as CodeBranchesAPI).id;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            checkIn = await CheckIn.Save(checkIn);

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
