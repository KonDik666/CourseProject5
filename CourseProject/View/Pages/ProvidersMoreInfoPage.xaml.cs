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
    /// Логика взаимодействия для ProvidersMoreInfoPage.xaml
    /// </summary>
    public partial class ProvidersMoreInfoPage : Page
    {
        Core db = new Core();
        providers currentProvider = new providers();
        public ProvidersMoreInfoPage(providers selectedProvider)
        {
            InitializeComponent();
            currentProvider = selectedProvider;
            providerNameTextBlock.Text = selectedProvider.provider_name;
            providerDateTextBlock.Text = "Ближайшая дата поставки: " + selectedProvider.supplies_date;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            medecinesOfProvidersDataGrid.ItemsSource = db.context.medicines.Where(x => x.providers_id_providers == currentProvider.id_providers).ToList();
        }
    }
}
