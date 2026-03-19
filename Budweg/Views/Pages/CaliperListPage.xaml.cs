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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Budweg.ViewModels;

namespace Budweg.Views.Pages
{
    /// <summary>
    /// Interaction logic for CaliperListPage.xaml
    /// </summary>
    public partial class CaliperListPage : Page
    {
        public CaliperListPage()
        {
            InitializeComponent();
            DataContext = new CaliperListViewModel();
        }
    }
}
