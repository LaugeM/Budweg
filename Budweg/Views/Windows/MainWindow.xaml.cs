using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Budweg.Views.Pages;

namespace Budweg.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new FrontPage();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_register.Visibility = Visibility.Collapsed;
                tt_overview.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_register.Visibility = Visibility.Visible;
                tt_overview.Visibility = Visibility.Visible;
            }
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void registerListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new RegCaliperPage();
        }

        private void homeListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new FrontPage();
        }

        private void overviewListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new CaliperListPage();
        }
    }
}
