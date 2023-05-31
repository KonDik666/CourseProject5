using CourseProject.View.Model;
using System;
using System.Collections.Generic;
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
using StringCheckLibrary;

namespace CourseProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewMedecinePage.xaml
    /// </summary>
    public partial class NewMedecinePage : Page
    {
        Core db = new Core();
        int selectedIdProvider;
        public int addOrEdit = 0;  //переменная проверяющая добавляет ли пользователь в базу или редактирует
        public medicines currentMedicine = new medicines();
        public NewMedecinePage(medicines selectedMedicine)
        {

            InitializeComponent();
            List<string> bbb = new List<string>();
            List<providers> providersArray = db.context.providers.ToList();
            string[] aaa = new string[providersArray.Count];   //получение списка названий поставщиков для combobox
            for (int i = 0; i < providersArray.Count; i++)
            {
                aaa[i] += providersArray[i].provider_name;
            }
            
            providersComboBox.ItemsSource = aaa;   //привязка данных полученного массива к combobox

            if(selectedMedicine != null) //заполнение полей данными для редактирвоания
            {
                nameTextBloxk.Text = selectedMedicine.medicine_name;
                atxCodeTextBlock.Text = selectedMedicine.medicine_code;
                costTextBlock.Text = selectedMedicine.cost.ToString();
                saleCostTextBlock.Text = selectedMedicine.sale_cost.ToString();
                amountTextBlock.Text = selectedMedicine.amount;
                descriptionTextBlock.Text = selectedMedicine.description;
                currentMedicine = selectedMedicine;
                addOrEdit = 1;
            }
            


        }

        private void providersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            providers curentProvider = db.context.providers.Where(x=>x.provider_name==providersComboBox.SelectedItem.ToString()).FirstOrDefault();
          
            selectedIdProvider = curentProvider.id_providers;
            Console.WriteLine(selectedIdProvider);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int checking = 0;
            if (CheckClass.CheckNameString(nameTextBloxk.Text)==true)
            {
                checking++;
            }
            else
            {
                nameTextBloxk.Text = "";
                MessageBox.Show("Введите название препарата русскими буквами без символов и цифр");
            }
            if (CheckClass.CheckMedecineCode(atxCodeTextBlock.Text) == true)
            {
                checking++;
            }
            else
            {
                atxCodeTextBlock.Text = "";
                MessageBox.Show("Введите медицинский код препарата английской буквой из существующих кодов");
            }
            if (CheckClass.OnlyDigits(costTextBlock.Text) == true)
            {
                checking++;
            }
            else
            {
                costTextBlock.Text = "";
                MessageBox.Show("Введите стоимость препарата в руб, без символов и букв");
            }
            if (CheckClass.OnlyDigits(saleCostTextBlock.Text) == true)
            {
                if (Convert.ToInt32(costTextBlock.Text) > Convert.ToInt32(saleCostTextBlock.Text))
                {
                    checking++;
                }
                else
                {
                    saleCostTextBlock.Text = "";
                    MessageBox.Show("Скидочная стоимость не должна превышать обычную");
                }
                
                   
            }
            else
            {
                saleCostTextBlock.Text = "";
                MessageBox.Show("Введите скидочную стоимость препарата в руб, без символов и букв");
            }
           


            if (addOrEdit==0 && checking==4)
            {
                medicines applyMedecine = new medicines();   //добавление нового лекарства в базу




                applyMedecine.medicine_name = nameTextBloxk.Text;
                applyMedecine.medicine_code = atxCodeTextBlock.Text;
                applyMedecine.cost = Convert.ToInt32(costTextBlock.Text);
                applyMedecine.sale_cost = Convert.ToInt32(saleCostTextBlock.Text);
                applyMedecine.amount = amountTextBlock.Text;
                applyMedecine.description = descriptionTextBlock.Text;

                applyMedecine.providers_id_providers = selectedIdProvider;

                db.context.medicines.Add(applyMedecine);
                db.context.SaveChanges();
                MessageBox.Show("Новое лекарство было успешно добавлено в базу");
                NavigationService.GoBack();
            }
           
            else if (addOrEdit == 1 && checking == 4)  //редактирование существующего лекарства
            {
                currentMedicine.medicine_name = nameTextBloxk.Text;
                currentMedicine.medicine_code = atxCodeTextBlock.Text;

                currentMedicine.cost = Convert.ToInt32(costTextBlock.Text);
                currentMedicine.sale_cost = Convert.ToInt32(saleCostTextBlock.Text);
                currentMedicine.amount = amountTextBlock.Text;
                currentMedicine.description = descriptionTextBlock.Text;

                currentMedicine.providers_id_providers = selectedIdProvider;

                db.context.medicines.AddOrUpdate(currentMedicine);
                db.context.SaveChanges();
                MessageBox.Show("Информация о лекарстве обновлена");
                NavigationService.GoBack();
            }

        }

       
    }
}
