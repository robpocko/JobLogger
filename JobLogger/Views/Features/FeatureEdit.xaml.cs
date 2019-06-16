using JobLogger.AppSystem;
using JobLogger.AppSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace JobLogger.Views.Features
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeatureEdit : Page
    {
        private FeatureAPI feature;

        public FeatureEdit()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboSources();
            SyncCombos();
            try
            {
                requirementList.ItemsSource = feature.requirements;
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                feature = e.Parameter as FeatureAPI;
                long ID;
                long.TryParse(feature.id, out ID);
                if (ID > 0)
                {
                    TextFeatureID.IsReadOnly = true;
                }
            }
            else
            {
                feature = new FeatureAPI();
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
            if (feature.status > 0)
            {
                for (int i = 0; i < ComboBoxStatus.Items.Count; i++)
                {
                    if ((RequirementStatus)(ComboBoxStatus.Items[i]) == feature.status)
                    {
                        ComboBoxStatus.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            feature = await Feature.Save(feature);

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void AddRequirementButton_Click(object sender, RoutedEventArgs e)
        {
            RequirementAPI requirement = new RequirementAPI { featureID = long.Parse(feature.id), status = RequirementStatus.Proposed, isNew = true };

            ((Frame)Parent).Navigate(
                typeof(Requirements.RequirementEdit),
                requirement,
                new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
