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

        private void CaliperOverviewButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RegCaliperButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RegCaliperPage();
        }
    }
}
