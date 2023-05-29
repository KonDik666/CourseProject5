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
    /// Логика взаимодействия для MedecinesPage.xaml
    /// </summary>
    public partial class MedecinesPage : Page
    {
        Core db = new Core();
        //List<medicines> arrayMedicines;
        public MedecinesPage()
        {
            InitializeComponent();
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            MedecinesDataGrid.ItemsSource = db.context.medecines_availability.ToList();
            MedecinesDataGrid.ItemsSource = db.context.medicines.ToList();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            
            var currentMedicine = MedecinesDataGrid.SelectedItem as medicines;
            App.selectedCurrentMedecine = MedecinesDataGrid.SelectedItem as medicines;
            NavigationService.Navigate(new MedecineInfoPage(currentMedicine));
           
            
        }

        private void code_A_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "a").ToList();

        }

        private void code_B_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "b").ToList();
        }

        private void code_C_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "c").ToList();
        }

        private void code_D_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "d").ToList();
        }

        private void code_G_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "g").ToList();
        }

        private void code_H_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "h").ToList();
        }

        private void code_J_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "j").ToList();
        }

        private void code_L_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "l").ToList();
        }

        private void code_M_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "m").ToList();
        }

        private void code_N_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "n").ToList();
        }

        private void code_P_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "p").ToList();
        }

        private void code_R_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "r").ToList();
        }

        private void code_S_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "s").ToList();
        }

        private void code_V_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p => p.medicine_code == "v").ToList();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.ToList();
        }

        private void searchTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            searchTextBox.Text = "";
        }

        private void searchTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            searchTextBox.Text = "Поиск лекарства...";
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MedecinesDataGrid.ItemsSource = db.context.medicines.Where(p=>p.medicine_name == searchTextBox.Text || p.medicine_name.Contains(searchTextBox.Text)).ToList();
        }

        private void addMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewMedecinePage(null));
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данное лекарство?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var SelectedMedicine = MedecinesDataGrid.SelectedItem as medicines;
                db.context.medicines.Remove(SelectedMedicine);
                db.context.SaveChanges();

                
                MessageBox.Show("Лекарство успешно удалено");
                this.NavigationService.Navigate(new MedecinesPage());
            }
        }

        private void redactButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMedicine = MedecinesDataGrid.SelectedItem as medicines;
            NavigationService.Navigate(new NewMedecinePage(selectedMedicine));
        }

        private void addToPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            medicines selectedMedecine = MedecinesDataGrid.SelectedItem as medicines;
            medecines_availability availabilityOrNot = db.context.medecines_availability.Where(x => x.adresses_id_adresses == App.currentAdressName.id_adresses && x.medecines_id_medecines == selectedMedecine.id_medicines).FirstOrDefault();
          
            if (MessageBox.Show("Добавить " + selectedMedecine.medicine_name + " к покупке?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(availabilityOrNot != null)
                {
                    bool containsOrNot = false;
                    for (int i = 0; i < App.selectedToOrderMedecines.Count; i++)
                    {
                        if (App.selectedToOrderMedecines[i].id_medicines == selectedMedecine.id_medicines)
                        {
                            containsOrNot = true;
                        }
                    }
                    if (containsOrNot == false)
                    {
                        App.selectedToOrderMedecines.Add(selectedMedecine);
                        MessageBox.Show("Добавлено к товарам!");
                    }
                    else
                    {
                        MessageBox.Show("Выбранное лекарство уже находится окне покупки");
                    }
                }
                else
                {
                    MessageBox.Show("Выбранное лекарство отсутсвует по ткущему адресу");
                }
               



            }
        }
    }
}
