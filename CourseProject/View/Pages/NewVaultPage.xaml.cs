using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
using CourseProject.View.Model;
using StringCheckLibrary;

namespace CourseProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewVaultPage.xaml
    /// </summary>
    public partial class NewVaultPage : Page
    {
        Core db = new Core();
        int checking = 0;
        public int addOrEdit = 0;
        public adresses currentAdresses = new adresses();
        public NewVaultPage(adresses selectedAdress)
        {
            InitializeComponent();
            buildingTypeComboBox.ItemsSource = new string[] { "Аптека", "Склад"};

            if(selectedAdress != null)
            {
                cityTextBox.Text = selectedAdress.city;
                adressTextBox.Text = selectedAdress.adress;
                //buildingTypeComboBox.SelectedItem = selectedBuilding.building_type;
                currentAdresses = selectedAdress;
                addOrEdit = 1;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckClass.CheckNameString(cityTextBox.Text) == false)
            {
                cityTextBox.Text = "";
                MessageBox.Show("Неправильно введен город");
                
            }
            else
            {
                checking += 1;
            }

            //if (CheckClass.CheckNameString(adressTextBox.Text) == false)
            //{
            //    adressTextBox.Text = "";
            //    MessageBox.Show("Неправильно введен адрес");

            //}
            //else
            //{
            //    checking += 1;
            //}

            if(buildingTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать тип строения");
            }
            else
            {
                checking += 1;
            }

            //добавление нового строения в базу

            if(checking == 2 && addOrEdit==0)
            {
                adresses vault = new adresses();

                

                vault.city = cityTextBox.Text;
                vault.adress = adressTextBox.Text;

                if (buildingTypeComboBox.SelectedItem.ToString() == "Аптека")
                {
                    vault.building_id_building = 1;
                }
                else
                {
                    vault.building_id_building = 2;
                }

                //var currentBuilding = buildingTypeComboBox.SelectedItem.ToString() as building;

                

                db.context.adresses.Add(vault);
                
                db.context.SaveChanges();
                MessageBox.Show("В базу был добавлен новый адрес");
                NavigationService.GoBack();
            }

            else if(checking == 2 && addOrEdit == 1)
            {
                Console.WriteLine(currentAdresses.building_id_building);

                
                currentAdresses.city = cityTextBox.Text;
                currentAdresses.adress = adressTextBox.Text;



                //var currentBuilding = buildingTypeComboBox.SelectedItem.ToString() as building;




                db.context.adresses.AddOrUpdate(currentAdresses);
                db.context.SaveChanges();
                MessageBox.Show("В базу был добавлен новый адрес");
                NavigationService.GoBack();
            }


        }
    }
}
