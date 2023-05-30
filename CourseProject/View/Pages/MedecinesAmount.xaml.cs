using CourseProject.View.Model;
using StringCheckLibrary;
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
    /// Логика взаимодействия для MedecinesAmount.xaml
    /// </summary>
    public partial class MedecinesAmount : Page
    {
        Core db = new Core();
        medicines curentMedicine = new medicines();
        public MedecinesAmount(medicines selectedMedecine)
        {
            InitializeComponent();                                                                            //получение информации о выбранном препарате
            List<medecines_availability> medAvailabilityAll = db.context.medecines_availability.ToList(); 
            curentMedicine = selectedMedecine;
            App.currentMedPreparate = selectedMedecine;
           
           
            List<medecines_availability> medAvailability = medAvailabilityAll.Where(x => x.medecines_id_medecines == selectedMedecine.id_medicines).ToList();

         //вывод текстблока с названием выбранного перпарата
            TextBlock medecineName = new TextBlock
            {
                Text = selectedMedecine.medicine_name,
                FontSize = 30,
                TextAlignment = TextAlignment.Center,

            };
            medNameStackPanel.Children.Add(medecineName); //добавление текстблоков в стакпанель


            List<adresses> medAdressesAll = db.context.adresses.ToList();
            List<adresses> medAdresses = new List<adresses>();
            string[] sss = new string[medAdressesAll.Count];
            int[] aaaa = new int[medAdressesAll.Count];

            for (int i = 0; i < medAdressesAll.Count; i++)    //создание текстблоков с названием адресов, за которыми закрпаелено выбранное лекарство, колличеством выбранного препарат на каждом адресе
            {


                
                for(int j = 0; j < medAvailability.Count; j++)
                {
                    if (medAvailability[j].adresses_id_adresses == medAdressesAll[i].id_adresses)
                    {
                        TextBlock adressName = new TextBlock
                        {
                            Text = "По адресу "+medAdressesAll[i].adress.ToString()+" находится ",
                            FontSize = 20,
                            TextAlignment = TextAlignment.Center,
                           


                        };
                        adressNameStackPanel.Children.Add(adressName);


                        TextBlock amount = new TextBlock
                        {
                            Text = medAvailability[j].amount_of_medecines.ToString()+" шт",
                            FontSize = 20,
                            TextAlignment = TextAlignment.Center,


                        };
                        amountStackPanel.Children.Add(amount); //добавление текстблоков в стакпанель

                        Button changeButton = new Button
                        {
                            Content = "Изменить",
                            Name = "i"+medAvailability[j].id_medecines_availability.ToString(),    //создание кнопок для изменения колличесва препарата
                            Margin = new Thickness(0, 10, 0 ,0),
                        };
                        changeButtonsStackPanel.Children.Add(changeButton);
                        changeButton.Click += ChangeButton_Click;
                        


                    }
                }

               

            }



          

          
           





        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
          
            Button changeButton = (Button)sender;
            int firstIndex = changeButton.Name.IndexOf('i');  //считывание индекса кнопки
            string index;
            index = changeButton.Name.Substring(firstIndex+1);
     
           
           
           
            int buttonIndex = Convert.ToInt32(index);
            Console.WriteLine(buttonIndex);
           
            medecines_availability currentMedecinesAvailability = db.context.medecines_availability.Where(x=>x.id_medecines_availability==buttonIndex).FirstOrDefault();


            MedecinesNewAmountDialogWindow dialogWindow = new MedecinesNewAmountDialogWindow();   //открытие диалогового окна для установки нового значения для колличества препарата по выбранному адресу

            if (dialogWindow.ShowDialog() == true)
            {
                if (CheckClass.OnlyDigits(dialogWindow.Value) == true)
                {
                    currentMedecinesAvailability.amount_of_medecines = dialogWindow.Value;
                    db.context.SaveChanges();
                    MessageBox.Show("Успешно обновлена информация о колличестве");

                    NavigationService.Navigate(new MedecinesAmount(curentMedicine));
                   
                }
                else
                {
                    MessageBox.Show("Неправильно введено колличество(печатать нужно без <шт>)");
                }
            }
        }

        private void newAddressOfMedecineButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewMedecineAddressPage());  //переход на страницу с закрпелением адреса за лекарством
        }
    }
}
