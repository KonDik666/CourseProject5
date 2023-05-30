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
using Microsoft.Office.Interop.Excel;
using Page = System.Windows.Controls.Page;
using Button = System.Windows.Controls.Button;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseProject.View.Pages
{
	/// <summary>
	/// Логика взаимодействия для SalesPage.xaml
	/// </summary>
	public partial class SalesPage : Page
	{
		Core db = new Core();
		public int dayOrInterval = 0;
		public SalesPage()
		{
			InitializeComponent();
            if (App.CurrentUser.role == "stuff")
            {
                reportIntervalButton.IsEnabled = false;  //если роль пользователя stuff, изменение доступности кнопки формирвоания отчета за временной интервал
            }
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			SalesDataGrid.ItemsSource=db.context.orders.ToList();  //формирвоание датагрид на оснвое таблицы orders
		}

		private void moreOrderInfoButton_Click(object sender, RoutedEventArgs e)
		{
			var selectedOrder = SalesDataGrid.SelectedItem as orders;  //переход на старницу с подробным описанием выбранного заказа
			NavigationService.Navigate(new MoreOrderInfoPage(selectedOrder));

		}

		private void reportButton_Click(object sender, RoutedEventArgs e)   //создание отчета по выбранной дате
		{
			ChoosingDateDialogWindow chooseDate = new ChoosingDateDialogWindow();  //открытие диалогового окна для выбора даты
			chooseDate.ShowDialog();
			string datte = chooseDate.Value;
			DateTime rtr = Convert.ToDateTime(datte);

            List<orders> selectedTimeOrders = db.context.orders.Where(x => x.order_date == rtr).ToList();
			List<ordered_medecines> selectedOrdrMed = new List<ordered_medecines>();
			List<ordered_medecines> allOrderedMed = db.context.ordered_medecines.ToList();
            Console.WriteLine("общ" + allOrderedMed.Count);
            for (int i= 0; i < allOrderedMed.Count; i++)   //сортирвовка данных из ьаблицы ordered_medecines по выбранной пользователем дате
			{
				
   
				for(int j=0; j<selectedTimeOrders.Count; j++)
				{
                    int ur = selectedTimeOrders[j].id_orders;
                    if (allOrderedMed[i].orders_id_orders == ur)
					{
						selectedOrdrMed.Add(allOrderedMed[i]);
						Console.WriteLine("erre"+ allOrderedMed[i].medicines_id_medicines);
					}
				}

            }
           
			int counter = 0;

            for (int i = 0; i < selectedTimeOrders.Count; i++)
			{
				counter += Convert.ToInt32(selectedTimeOrders[i].order_sum);
			}
			int[] sumMed = new int[selectedOrdrMed.Count];
			List<int> sumMed2 = new List<int>();   //подсчет кол-ва препаратов

			
			List<medicines> existingMedicines = db.context.medicines.ToList();
            List<ordered_medecines> selected = new List<ordered_medecines>();
			int count = 0;

			Console.WriteLine(selectedOrdrMed.Count);
			List<int> idPreparates = new List<int>();
            List<int> amountPreparates = new List<int>();

            //получение инофрмации об общем коллчисевте каждого препарата по выбранной дате
            for (int i= 0; i < selectedOrdrMed.Count; i++)
			{
				bool r = true;
				for(int j=0; j<sumMed2.Count; j++)
				{
					if (selectedOrdrMed[i].medicines_id_medicines == sumMed2[j])
					{
						r = false;
					}
				}
				if (r == true)
				{
                    for (int j = 0; j < selectedOrdrMed.Count; j++)
                    {
                        if (selectedOrdrMed[i].medicines_id_medicines == selectedOrdrMed[j].medicines_id_medicines)
                        {
                            count += Convert.ToInt32(selectedOrdrMed[j].selected_medecines_amount);  //подсчет колличества i-го препарата
                        }
					

					}
					sumMed2.Add(Convert.ToInt32(selectedOrdrMed[i].medicines_id_medicines));
					idPreparates.Add(Convert.ToInt32(selectedOrdrMed[i].medicines_id_medicines));  //занемение в массив id всех лекарств во временном прмежутке
					amountPreparates.Add(count);  //занесение в в массив колличества каждого лекарства во временном прмежутке
                    Console.WriteLine("}}" +selectedOrdrMed[i].medicines_id_medicines + "  " + count);
					count = 0;
                }

				
			
			}
			List<medicines> medicinesOfIdPreparates = new List<medicines>();
			for(int i=0; i<idPreparates.Count; i++)
			{
				int ur = idPreparates[i];

                medicines addingMed = db.context.medicines.Where(x => x.id_medicines == ur).FirstOrDefault();
				medicinesOfIdPreparates.Add(addingMed);
			}
			Console.WriteLine(idPreparates.Count + "  " + amountPreparates.Count + "ddd "+ medicinesOfIdPreparates[1].medicine_name);
			
			
            //Вывод отчета в Excel
        

            //создаем файл Excel
            Excel.Application aplication = new Excel.Application();
            aplication.Visible = true;
            //количество листов
            aplication.SheetsInNewWorkbook = 1;
            //добавляем рабочую книгу
            Excel.Workbook workbook = aplication.Workbooks.Add(Type.Missing);
            //создаем лист
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Name = "Отчет";

            Excel.Range range = worksheet.get_Range("A1", "E1");
            Excel.Range range2 = worksheet.get_Range("A2", "E2");
            Excel.Range range3 = worksheet.get_Range("D7", "E7");
            range.Merge(Type.Missing);
			range2.Merge(Type.Missing);
            range3.Merge(Type.Missing);
            range.Value = "Отчет по продажам товаров за "+ datte;
			range2.Value = "За данную дату продано:";
			range3.Value = "Прибыль: " + counter + " руб";
			
			for(int i=2; i < medicinesOfIdPreparates.Count+2; i++)
			{
				
                    worksheet.Cells[i + 1, 1] = medicinesOfIdPreparates[i-2].medicine_name;
                    worksheet.Cells[i + 1, 3] = amountPreparates[i-2]+" шт";


            }


        }

		private void reportIntervalButton_Click(object sender, RoutedEventArgs e)  //создание отчета по временному промежутку(создается так же как и предыдущий с изменением конкретной даты на интервал)
		{
			ChoosingDateIntervalDialogWindow chooseDateInteval = new ChoosingDateIntervalDialogWindow();
			chooseDateInteval.ShowDialog();
			string date1 = chooseDateInteval.Value1;
			string date2 = chooseDateInteval.Value2;
			DateTime datetime1 = Convert.ToDateTime(date1);
			DateTime datetime2 = Convert.ToDateTime(date2);
			Console.WriteLine("чуч"+ date1);
            Console.WriteLine("чуч" + date2);
            List<orders> selectedTimeOrders = db.context.orders.Where(x => x.order_date>=datetime1 && x.order_date<=datetime2).ToList();
			Console.WriteLine("чучсмек"+ selectedTimeOrders.Count);
            List<ordered_medecines> selectedOrdrMed = new List<ordered_medecines>();
            List<ordered_medecines> allOrderedMed = db.context.ordered_medecines.ToList();
            Console.WriteLine("общ" + allOrderedMed.Count);
            for (int i = 0; i < allOrderedMed.Count; i++)
            {


                for (int j = 0; j < selectedTimeOrders.Count; j++)
                {
                    int ur = selectedTimeOrders[j].id_orders;
                    if (allOrderedMed[i].orders_id_orders == ur)
                    {
                        selectedOrdrMed.Add(allOrderedMed[i]);
                        Console.WriteLine("erre" + allOrderedMed[i].medicines_id_medicines);
                    }
                }

            }

            int counter = 0;

            for (int i = 0; i < selectedTimeOrders.Count; i++)
            {
                counter += Convert.ToInt32(selectedTimeOrders[i].order_sum);
            }
            int[] sumMed = new int[selectedOrdrMed.Count];
            List<int> sumMed2 = new List<int>();   //подсчет кол-ва препаратов


            List<medicines> existingMedicines = db.context.medicines.ToList();
            List<ordered_medecines> selected = new List<ordered_medecines>();
            int count = 0;

            Console.WriteLine(selectedOrdrMed.Count);
            List<int> idPreparates = new List<int>();
            List<int> amountPreparates = new List<int>();

            for (int i = 0; i < selectedOrdrMed.Count; i++)
            {
                bool r = true;
                for (int j = 0; j < sumMed2.Count; j++)
                {
                    if (selectedOrdrMed[i].medicines_id_medicines == sumMed2[j])
                    {
                        r = false;
                    }
                }
                if (r == true)
                {
                    for (int j = 0; j < selectedOrdrMed.Count; j++)
                    {
                        if (selectedOrdrMed[i].medicines_id_medicines == selectedOrdrMed[j].medicines_id_medicines)
                        {
                            count += Convert.ToInt32(selectedOrdrMed[j].selected_medecines_amount);
                        }


                    }
                    sumMed2.Add(Convert.ToInt32(selectedOrdrMed[i].medicines_id_medicines));
                    idPreparates.Add(Convert.ToInt32(selectedOrdrMed[i].medicines_id_medicines));
                    amountPreparates.Add(count);
                    Console.WriteLine("}}" + selectedOrdrMed[i].medicines_id_medicines + "  " + count);
                    count = 0;
                }



            }
            List<medicines> medicinesOfIdPreparates = new List<medicines>();
            for (int i = 0; i < idPreparates.Count; i++)
            {
                int ur = idPreparates[i];

                medicines addingMed = db.context.medicines.Where(x => x.id_medicines == ur).FirstOrDefault();
                medicinesOfIdPreparates.Add(addingMed);
            }
            Console.WriteLine(idPreparates.Count + "  " + amountPreparates.Count + "ddd " + medicinesOfIdPreparates[1].medicine_name);





            //создаем файл Excel
            Excel.Application aplication = new Excel.Application();
            aplication.Visible = true;
            //количество листов
            aplication.SheetsInNewWorkbook = 1;
            //добавляем рабочую книгу
            Excel.Workbook workbook = aplication.Workbooks.Add(Type.Missing);
            //создаем лист
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Name = "Отчет";

            Excel.Range range = worksheet.get_Range("A1", "E1");
            Excel.Range range2 = worksheet.get_Range("A2", "E2");
            Excel.Range range3 = worksheet.get_Range("D7", "E7");
            range.Merge(Type.Missing);
            range2.Merge(Type.Missing);
            range3.Merge(Type.Missing);
            range.Value = "Отчет по продажам товаров за интервал с" + date1 +" по" + date2;
            range2.Value = "За данный интервал продано:";
            range3.Value = "Прибыль: " + counter + " руб";

            for (int i = 2; i < medicinesOfIdPreparates.Count + 2; i++)
            {

                worksheet.Cells[i + 1, 1] = medicinesOfIdPreparates[i - 2].medicine_name;
                worksheet.Cells[i + 1, 3] = amountPreparates[i - 2] + " шт";


            }



        }
	}
}
