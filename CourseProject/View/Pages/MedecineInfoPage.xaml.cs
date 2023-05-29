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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedecineInfoPage.xaml
    /// </summary>
    public partial class MedecineInfoPage : Page

    {
        Core db = new Core();
        medicines selectedMedecine = new medicines();
        public MedecineInfoPage(medicines currentMedicine)
        {
            selectedMedecine = currentMedicine;
            //вывод информации о текущем лекарстве
            medecines_availability result;
            int result3;
            List<medecines_availability> resultsArray = new List<medecines_availability>();
            providers provName = db.context.providers.Where(x => x.id_providers == selectedMedecine.providers_id_providers).FirstOrDefault();
            InitializeComponent();
            int currentId = currentMedicine.id_medicines;   //id текщего препарата
            descTextBlock.Text = currentMedicine.description; //описание текщего препарата
            costTextBlock.Text = currentMedicine.cost.ToString(); // цена текущего препарата
            saleCostTextBlock.Text = currentMedicine.sale_cost.ToString(); //скидочная цена текущего препарата
            medecineNameTextBlock.Text = currentMedicine.medicine_name; // наименование препарата
            if(provName != null)
            {
                providerNameTextBlock.Text = provName.provider_name; //наименование поставщика препарата
            }
            else
            {
                providerNameTextBlock.Text = "Поставщик не найден";
            }
           
            List<photo> photos = db.context.photo.ToList();
            List<photo> tyty = photos.Where(x=>x.medicines_id_medicines == currentId).ToList();
            Console.WriteLine(tyty.Count);
            if (tyty.Count>0)
            {
                List<photo> photoList = new List<photo>();
                photoList = db.context.photo.Where(x => x.medicines_id_medicines == currentMedicine.id_medicines).ToList();
                string[] codes = new string[photoList.Count];
                for (int i = 0; i < photoList.Count; i++)
                {
                    codes[i] += photoList[i].path;
                }
                string codee = codes[0];

                string b = string.Empty;
                int val = 0;
                int firstIndex = codee.IndexOf('(');
                codee = codee.Substring(0, firstIndex);

                for (int i = 0; i < codee.Length; i++)
                {
                    if (Char.IsDigit(codee[i]))
                    {
                        b += codee[i];
                    }


                }
                if (b.Length > 0)
                {
                    val = int.Parse(b);
                    Console.WriteLine(val);
                }



                List<photo> ere = new List<photo>();




                ere = db.context.photo.Where(x => x.medicines_id_medicines == val).ToList();

                if (ere.Count == 2)
                {
                    Uri src = new Uri(ere[0].path, UriKind.Relative);
                    BitmapImage bImg = new BitmapImage(src);

                    image1.Source = bImg;

                    Uri src2 = new Uri(ere[1].path, UriKind.Relative);
                    BitmapImage bImg2 = new BitmapImage(src2);

                    image2.Source = bImg2;
                }
                else
                {

                    Uri src = new Uri(ere[0].path, UriKind.Relative);
                    BitmapImage bImg = new BitmapImage(src);

                    image1.Source = bImg;

                }




                Console.WriteLine(ere[0].path);
            }
            else
            {
                Uri src = new Uri("../../Assets/Images/logo.png", UriKind.Relative);
                BitmapImage bImg = new BitmapImage(src);

                image1.Source = bImg;

                Uri src2 = new Uri("../../Assets/Images/logo.png", UriKind.Relative);
                BitmapImage bImg2 = new BitmapImage(src2);

                image2.Source = bImg2;
            }

                    


            result = (medecines_availability)db.context.medecines_availability.Where(x => x.medecines_id_medecines == currentId).FirstOrDefault(); //вывод колличества прперата(из таблицы medecines_availability)
            resultsArray = db.context.medecines_availability.Where(x => x.medecines_id_medecines == currentId).ToList();
           
            if (resultsArray != null)
            {
                medecines_availability result2 = resultsArray.Where(x => x.adresses_id_adresses == App.currentAdressName.id_adresses).FirstOrDefault();

                if (result2 != null)
                {
                    amountTextBlock.Text = result2.amount_of_medecines;
                    
                    
                }
                else if(result2 == null)
                {
                    result3 = Convert.ToInt32(resultsArray.Max(x => x.amount_of_medecines));

                    amountTextBlock.Text = result3.ToString();
                }
                
            }
            else
            {
                amountTextBlock.Text = "нет в наличии";
            }

            

        }

        private void amountInfoButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MedecinesAmount(selectedMedecine));
        }

        private void orderAddButton_Click(object sender, RoutedEventArgs e)
        {
            medecines_availability availabilityOrNot = db.context.medecines_availability.Where(x => x.adresses_id_adresses == App.currentAdressName.id_adresses && x.medecines_id_medecines == selectedMedecine.id_medicines).FirstOrDefault();
            if (MessageBox.Show("Добавить " + selectedMedecine.medicine_name + " к покупке?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (availabilityOrNot != null)
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
