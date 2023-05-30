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
    /// Логика взаимодействия для NewMedecineAddressPage.xaml
    /// </summary>
    public partial class NewMedecineAddressPage : Page
    {
        Core db = new Core();
        public NewMedecineAddressPage()
        {
            InitializeComponent();
            addressesComboBox.ItemsSource = db.context.adresses.Select(p => p.adress).ToList(); //добавление названий адресов в комбобокс
        }

        private void addNewMedecineAddressButton_Click(object sender, RoutedEventArgs e)
        {
            medecines_availability addMedAvailability = new medecines_availability();

           //добавление нового адреса для лекарства

            addMedAvailability.amount_of_medecines = amountOfMedecinesTextBox.Text;

            adresses curAdr = db.context.adresses.Where(x => x.adress == addressesComboBox.SelectedItem.ToString()).FirstOrDefault();
            addMedAvailability.medecines_id_medecines = App.selectedCurrentMedecine.id_medicines;
            addMedAvailability.adresses_id_adresses = curAdr.id_adresses;
            List<medecines_availability> check = db.context.medecines_availability.Where(x => x.medecines_id_medecines == addMedAvailability.medecines_id_medecines & x.adresses_id_adresses == addMedAvailability.adresses_id_adresses).ToList();
            Console.WriteLine(check.Count);
            if (check.Count != 0)
            {
                MessageBox.Show("Данный адрес уже закреплен за этим лекарством");

            }
            else
            {
                db.context.medecines_availability.Add(addMedAvailability);
                db.context.SaveChanges();
                MessageBox.Show("Новый адрес успешно закрпелен за этим лекарством");
            }


        }
    }
}
