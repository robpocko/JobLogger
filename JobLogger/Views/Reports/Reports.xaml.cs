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
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JobLogger.Views.Reports
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Reports : Page
    {
        public Reports()
        {
            this.InitializeComponent();
        }

        private async void TimesheetToday_Click(object sender, RoutedEventArgs e)
        {
            List<TimesheetAPI> reportData = await Timesheet.Get(DateTime.Now.Date);
            selectedDate.Date = DateTime.Now.Date;
            report.ItemsSource = reportData;
            //DisplayTimesheet(reportData);
        }

        private async void TimesheetYesterday_Click(object sender, RoutedEventArgs e)
        {
            DateTime reportDate = DateTime.Now.Date;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    reportDate = reportDate.AddDays(-3);
                    break;
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                case DayOfWeek.Saturday:
                    reportDate = reportDate.AddDays(-1);
                    break;
                default:            // Sunday
                    reportDate = reportDate.AddDays(-2);
                    break;
            }

            List<TimesheetAPI> reportData = await Timesheet.Get(reportDate);
            selectedDate.Date = reportDate;
            report.ItemsSource = reportData;
           // DisplayTimesheet(reportData);
        }

        private async void TimesheetAnyDay_Click(object sender, RoutedEventArgs e)
        {
            List<TimesheetAPI> reportData = await Timesheet.Get(DateTime.Now.Date);
            report.ItemsSource = reportData;
            //DisplayTimesheet(reportData);
        }

        private void DisplayTimesheet(List<TimesheetAPI> reportData)
        {
            ReportContainer.Children.Clear();

            int[] widths = new int[] { 100, 100, 80, 300 };

            Grid grid = new Grid();

            grid.Margin = new Thickness(10, 50, 10, 50);
            grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(
                (byte)(Convert.ToUInt32("FF", 16)),
                (byte)(Convert.ToUInt32("1F", 16)),
                (byte)(Convert.ToUInt32("4C", 16)),
                (byte)(Convert.ToUInt32("B7", 16))));

            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(widths[i], GridUnitType.Auto);
                col.MinWidth = widths[i];

                grid.ColumnDefinitions.Add(col);

                TextBlock tblock = new TextBlock();
                tblock.HorizontalAlignment = HorizontalAlignment.Left;
                tblock.VerticalAlignment = VerticalAlignment.Center;
                tblock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                tblock.Margin = new Thickness(5, 5, 0, 5);

                tblock.Text = i == 0 ? "Start Time" : 
                              i == 1 ? "End Time" : 
                              i == 2 ? "Duration" :
                                       "Comment";

                grid.Children.Add(tblock);
                Grid.SetColumn(tblock, i);
            }

            ReportContainer.Children.Add(grid);

            ListView reportBody = new ListView();
            string sXAML = @"
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
<TextBlock Text=""Test"" />
<TextBlock Text=""One"" />
<TextBlock Text=""Two"" />
<TextBlock Text=""Three"" />
</DataTemplate>";
            var itemTemplate = XamlReader.Load(sXAML) as DataTemplate;
            reportBody.ItemTemplate = itemTemplate;
            ReportContainer.Children.Add(reportBody);

        }

        private async void SelectedDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            List<TimesheetAPI> reportData = await Timesheet.Get(selectedDate.Date.Value.Date);
            report.ItemsSource = reportData;
            //DisplayTimesheet(reportData);
        }

        private void TaskTimeSpent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
