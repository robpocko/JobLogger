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
            DisplayTimesheet(reportData);
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
            DisplayTimesheet(reportData);
        }

        private async void TimesheetAnyDay_Click(object sender, RoutedEventArgs e)
        {
            List<TimesheetAPI> reportData = await Timesheet.Get(DateTime.Now.Date);
        }

        private void DisplayTimesheet(List<TimesheetAPI> reportData)
        {
            ReportContainer.Children.Clear();



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
                if (i < 3)
                {
                    col.Width = new GridLength(0, GridUnitType.Auto);
                }
                else
                {
                    col.Width = new GridLength(1, GridUnitType.Star);
                }

                grid.ColumnDefinitions.Add(col);

                TextBlock tblock = new TextBlock();
                tblock.HorizontalAlignment = HorizontalAlignment.Left;
                tblock.VerticalAlignment = VerticalAlignment.Center;
                tblock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                tblock.Margin = new Thickness(5, 5, 0, 5);

                tblock.Text = i == 0 ?
                                "Start Time" : i == 1 ?
                                "End Time" : i == 2 ?
                                "Duration" :
                                "Comment";


                //    TextBlock tbox = new TextBlock { Margin = new Thickness(5, 5, 0, 5), Foreground = new SolidColorBrush(Windows.UI.Colors.White) };
                //    tbox.Text = "Hello";
                grid.Children.Add(tblock);
                Grid.SetColumn(tblock, i);
            }


            //ColumnDefinition col1 = new ColumnDefinition();
            //col1.Width = new GridLength(0, GridUnitType.Auto);
            //grid.ColumnDefinitions.Add(col1);
            //CheckBox cbox = new CheckBox();
            //cbox.MinWidth = 32;
            //cbox.HorizontalAlignment = HorizontalAlignment.Left;
            //cbox.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
            //grid.Children.Add(cbox);
            //Grid.SetColumn(cbox, 0);

            //ColumnDefinition col2 = new ColumnDefinition();
            //col2.Width = new GridLength(0, GridUnitType.Auto);
            //grid.ColumnDefinitions.Add(col2);
            //TextBlock tblock = new TextBlock();
            //tblock.FontSize = 16;
            //tblock.HorizontalAlignment = HorizontalAlignment.Left;
            //tblock.VerticalAlignment = VerticalAlignment.Center;
            //tblock.Text = "text";
            //grid.Children.Add(tblock);
            //Grid.SetColumn(tblock, 1);


            //ColumnDefinition col3 = new ColumnDefinition();
            //col3.Width = new GridLength(1, GridUnitType.Star);
            //grid.ColumnDefinitions.Add(col3);
            //TextBox tbox = new TextBox();
            //tbox.FontSize = 16;
            //tbox.HorizontalAlignment = HorizontalAlignment.Left;
            //tbox.VerticalAlignment = VerticalAlignment.Center;
            //grid.Children.Add(tbox);
            //Grid.SetColumn(tbox, 2);


            ReportContainer.Children.Add(grid);
        }
    }
}
