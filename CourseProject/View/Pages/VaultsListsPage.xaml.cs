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
    /// Логика взаимодействия для VaultsListsPage.xaml
    /// </summary>
    public partial class VaultsListsPage : Page
    {
        Core db = new Core();
        public VaultsListsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            adressesInfoGrid.ItemsSource = db.context.adresses.ToList();
        }

        private void addAdressButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewVaultPage(null));
        }

        private void changeAdressInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAdress = adressesInfoGrid.SelectedItem as adresses;
           
            NavigationService.Navigate(new NewVaultPage(selectedAdress));
        }

        private void removeAdressButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данного адрес? Это приведет к обнулении связанной с ним информации в базе", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                List<medecines_availability> lstMedAvailability = new List<medecines_availability>();
                var SelectedAdress = adressesInfoGrid.SelectedItem as adresses;
                lstMedAvailability = db.context.medecines_availability.Where(x => x.adresses_id_adresses == SelectedAdress.id_adresses).ToList();
                if (SelectedAdress.id_adresses == App.currentAdressName.id_adresses)
                {
                    MessageBox.Show("Удалить текущий адрес невозможно");
                }
                else
                {
                    for (int i = 0; i < lstMedAvailability.Count; i++)
                    {
                        db.context.medecines_availability.Remove(lstMedAvailability[i]);
                    }

                    db.context.adresses.Remove(SelectedAdress);
                    db.context.SaveChanges();

                    adressesInfoGrid.ItemsSource = db.context.user.ToList();
                    MessageBox.Show("Адрес успешно удален");
                }
              
            }
        }
    }
}
