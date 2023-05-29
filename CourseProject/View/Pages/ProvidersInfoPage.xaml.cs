using CourseProject.View.Model;
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
    /// Логика взаимодействия для ProvidersInfoPage.xaml
    /// </summary>
    public partial class ProvidersInfoPage : Page
    {
        Core db = new Core();

       
        public ProvidersInfoPage()
        {
            List<providers> providersAll = db.context.providers.ToList();
            InitializeComponent();
           
        }

        private void addProviderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewProviderPage(null));
        }

        private void changeProviderButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = providersDataGrid.SelectedItem as providers;
            NavigationService.Navigate(new NewProviderPage(selectedProvider));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            providersDataGrid.ItemsSource = db.context.providers.ToList();
        }

        private void removeProviderButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = providersDataGrid.SelectedItem as providers;
            List<medicines> providersMedicines = db.context.medicines.Where(x => x.providers_id_providers == selectedProvider.id_providers).ToList();
            if (MessageBox.Show("Удаление поставщика приведет к обнулению информации о поставляемой им продукции, вы уверены что хотите удалить поставщика?","Предупреждение!", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
               
                db.context.providers.Remove(selectedProvider);
                db.context.SaveChanges();

                providersDataGrid.ItemsSource = db.context.providers.ToList();
            }
            
        }

        private void moreProviderInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProvider = providersDataGrid.SelectedItem as providers;
            NavigationService.Navigate(new ProvidersMoreInfoPage(selectedProvider));
        }
    }
}
