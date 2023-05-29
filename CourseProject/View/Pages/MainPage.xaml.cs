using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if (App.CurrentUser.role == "stuff")
            {
                usersButton.IsEnabled = false;
                providersInfoButton.IsEnabled = false;
            }
        }

        private void medecinesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedecinesPage());
        }

        private void usersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }

        private void adressesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VaultsListsPage());
        }

        private void purchaseButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PurchasePage());
        }

        private void providersInfoButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProvidersInfoPage());
        }

		private void salesInfoButton_Click(object sender, RoutedEventArgs e)
		{
            NavigationService.Navigate(new SalesPage());
		}
	}
}
