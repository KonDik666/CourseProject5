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
	/// Логика взаимодействия для MoreOrderInfoPage.xaml
	/// </summary>
	public partial class MoreOrderInfoPage : Page
	{
		Core db = new Core();
		public MoreOrderInfoPage(orders SelectedOrder)  //вывод подробной информации о заказе
		{
			InitializeComponent();

            //вывод текстблоков с информацией о заказе
			TextBlock orderNumber = new TextBlock
			{
                Text = "Покупка No "+SelectedOrder.id_orders,
                FontSize = 30,
                TextAlignment = TextAlignment.Center,
            };
            orderNoStackPanel.Children.Add(orderNumber);

            TextBlock orderDate = new TextBlock
            {
                Text = "Дата " + SelectedOrder.order_date,
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
            };
            orderDateStackPanel.Children.Add(orderDate);

            TextBlock orderTime = new TextBlock
            {
                Text = "Время " + SelectedOrder.order_date,
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
            };
            orderTimeStackPanel.Children.Add(orderTime);

            user orderUser = db.context.user.Where(x=>x.id_user==SelectedOrder.user_id_user).FirstOrDefault();
            TextBlock userInfo = new TextBlock
            {
                Text = "Пользователь "+ orderUser.name+" "+ orderUser.surname,
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
            };
            userInfoStackPanel.Children.Add(userInfo);

            adresses orderAdress = db.context.adresses.Where(x=>x.id_adresses==SelectedOrder.adresses_id_adresses).FirstOrDefault();
            TextBlock orderAdresses = new TextBlock
            {
                Text = "Адрес " + orderAdress.adress,
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
            };
            adressInfoStackPanel.Children.Add(orderAdresses);

            List<ordered_medecines> ordrMed = db.context.ordered_medecines.Where(x=>x.orders_id_orders==SelectedOrder.id_orders).ToList();
            Console.WriteLine("erer" + ordrMed.Count);
            List<medicines> medNames = new List<medicines>();
            medicines med = new medicines();
            //добавление текстблоков с информацией о лекарстввах в заказе
            for (int i=0; i < ordrMed.Count; i++)
            {
                int u = Convert.ToInt32(ordrMed[i].medicines_id_medicines);
               med = db.context.medicines.Where(x => x.id_medicines == u).FirstOrDefault();
                medNames.Add(med);

                TextBlock medecines = new TextBlock
                {
                    Text = medNames[i].medicine_name,
                    FontSize = 20,
                    TextAlignment = TextAlignment.Center,
                };
                medecineNameStackPanel.Children.Add(medecines);

                TextBlock amounts = new TextBlock
                {
                    Text = ordrMed[i].selected_medecines_amount.ToString(),
                    FontSize = 20,
                    TextAlignment = TextAlignment.Center,

                };
                medecineAmountStackPanel.Children.Add(amounts);
            }

            TextBlock fSum = new TextBlock
            {
                Text = "Итог " + SelectedOrder.order_sum.ToString(),
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
            };
            finalSumStackPanel.Children.Add(fSum);
            

        }
	}
}
