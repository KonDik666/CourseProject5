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

namespace CourseProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PurchasePage.xaml
    /// </summary>
    public partial class PurchasePage : Page
    {
        Core db = new Core();
        public TextBox amount = new TextBox();
        public List<TextBlock> costs = new List<TextBlock>();
        public List<TextBox> amounts = new List<TextBox>();
        public List<TextBlock> medeciness = new List<TextBlock>();
        public TextBlock finalCost = new TextBlock();
        public int[] sum = new int[App.selectedToOrderMedecines.Count];
        public string[] allMedecinesName = new string[App.selectedToOrderMedecines.Count];
        public PurchasePage()   //заполнение окна покупки текстблоками с информацией о всех выбранных фармацевтом лекарствах и их стоимости
        {
            InitializeComponent();

            for(int i=0; i<App.selectedToOrderMedecines.Count; i++)
            {
                TextBlock purchaseMedecine = new TextBlock
                {
                    Text = App.selectedToOrderMedecines[i].medicine_name,
                    FontSize = 20,
                    TextAlignment = TextAlignment.Center,
                    Name = "r" + i,
                };
                medeciness.Add(purchaseMedecine);
                medecinesNamesStackPanel.Children.Add(purchaseMedecine);

                TextBlock purchaseCost = new TextBlock
                {
                    Text = App.selectedToOrderMedecines[i].cost.ToString(),
                    FontSize = 20,
                    TextAlignment = TextAlignment.Center,
                    Name = "r"+i,
                };
                costs.Add(purchaseCost);
                medecinesCostsStackPanel.Children.Add(purchaseCost);

                TextBox amount = new TextBox
                {
                    Text = "1",
                    FontSize = 20,
                    TextAlignment = TextAlignment.Center,
                    Name = "r" + i,
                    Width = 100,


                };
                amounts.Add(amount);
             
                medecinesAmountsStackPanel.Children.Add(amount);

                
                //создание кнопок для удаления лекарств из окна покупки
                Button removeMedecineButton = new Button
                {
                    Content = "удалить",
                    FontSize = 15,
                    Name = "i" + i,
                };
                removeMedecinesButtonsStackPanel.Children.Add(removeMedecineButton);
                removeMedecineButton.Click += RemoveMedecineButton_Click;
            }

        
            
          
        }
        //удаление лекарства напротив кнопки
        private void RemoveMedecineButton_Click(object sender, RoutedEventArgs e)
        {
           Button removeMedecineButton = (Button)sender;
            int firstIndex = removeMedecineButton.Name.IndexOf('i'); //вычиление индекса кнопки
            string index;
            index = removeMedecineButton.Name.Substring(firstIndex + 1);
            int buttonIndex = Convert.ToInt32(index);
          
            medecinesNamesStackPanel.Children.RemoveAt(buttonIndex);
            medecinesCostsStackPanel.Children.RemoveAt(buttonIndex);
            medecinesAmountsStackPanel.Children.RemoveAt(buttonIndex);
           
            removeMedecinesButtonsStackPanel.Children.RemoveAt(buttonIndex);

            App.selectedToOrderMedecines.RemoveAt(buttonIndex);

            Console.WriteLine(buttonIndex);
            NavigationService.Navigate(new PurchasePage());
        }

      

        private void confirmOrderButton_Click(object sender, RoutedEventArgs e) //подтверждение и оформление покупки
        {
            orders newOrder = new orders();
            for (int i = 0; i < App.selectedToOrderMedecines.Count; i++)  //подсчет итоговой стоимотси покупки путем умножения цены препарат на выбранное пользоватлем колличество
            {
                sum[i] = Convert.ToInt32(costs[i].Text) * Convert.ToInt32(amounts[i].Text);
                Console.WriteLine(Convert.ToInt32(costs[i].Text));
                Console.WriteLine(Convert.ToInt32(amounts[i].Text));

                allMedecinesName[i] = medeciness[i].Text;
            }
            int finalSum = sum.Sum();  //иттговая сумма
           
            finalCostTextBlock.Text = finalSum.ToString();
            DateTime currentDate = DateTime.UtcNow.Date;
            DateTime currentTime = DateTime.Now;
            
           

            newOrder.order_date = Convert.ToDateTime(currentDate);
            newOrder.order_time = currentTime.ToShortTimeString();
            newOrder.user_id_user =App.CurrentUser.id_user;
            newOrder.order_sum = finalSum.ToString();
            newOrder.adresses_id_adresses = App.currentAdressName.id_adresses;
            var s=string.Empty;
            for (int i = 0; i < App.selectedToOrderMedecines.Count; i++)
            {
                s += App.selectedToOrderMedecines[i].medicine_name + ", ";
               
            }

           

            newOrder.medecines_list = s;
            ordered_medecines orderedMed = new ordered_medecines();
            List<medecines_availability> descendingAmounts = new List<medecines_availability>();
            medecines_availability addingMedAvail = new medecines_availability();

           
            //добавление закзаа
           
            db.context.orders.Add(newOrder);
            db.context.SaveChanges();

           

            for (int i=0; i < App.selectedToOrderMedecines.Count; i++)
            {
                int a = App.selectedToOrderMedecines[i].id_medicines;

               
                medecines_availability descendingAmount = db.context.medecines_availability.Where(x => x.medecines_id_medecines == a & x.adresses_id_adresses == App.currentAdressName.id_adresses).FirstOrDefault();
                descendingAmounts.Add(descendingAmount);
            }  
           
            Console.WriteLine("ytyt"+ descendingAmounts.Count);
            //добавление покупки в таблицу orders в бд, так же добавление информации о купленных в данном заказе лекарств в таблицу ordered_medecines
            for(int i=0; i < App.selectedToOrderMedecines.Count; i++)
            {

                
               
                descendingAmounts[i].amount_of_medecines = (Convert.ToInt32(descendingAmounts[i].amount_of_medecines) - Convert.ToInt32(amounts[i].Text)).ToString();
                db.context.medecines_availability.AddOrUpdate(descendingAmounts[i]);
                db.context.SaveChanges();
                Console.WriteLine("gtgtee" + descendingAmounts[i].amount_of_medecines);

                orderedMed.orders_id_orders = newOrder.id_orders;
                orderedMed.medicines_id_medicines = App.selectedToOrderMedecines[i].id_medicines;
                orderedMed.selected_medecines_amount = Convert.ToInt32(amounts[i].Text);
                db.context.ordered_medecines.Add(orderedMed);
                db.context.SaveChanges();
            }
            MessageBox.Show("Покупка No"+ newOrder.id_orders+" оформлена успешно");
           
        }
        //обновление стоимости
        private void refreshCost_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < App.selectedToOrderMedecines.Count; i++)
            {
                sum[i] = Convert.ToInt32(costs[i].Text) * Convert.ToInt32(amounts[i].Text);
                Console.WriteLine(Convert.ToInt32(costs[i].Text));
                Console.WriteLine(Convert.ToInt32(amounts[i].Text));
            }

            int finalSum = sum.Sum();
            Console.WriteLine(finalSum);
            finalCostTextBlock.Text = finalSum.ToString();

        }
    }
}
