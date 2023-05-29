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
		public SalesPage()
		{
			InitializeComponent();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			SalesDataGrid.ItemsSource=db.context.orders.ToList();
		}

		private void moreOrderInfoButton_Click(object sender, RoutedEventArgs e)
		{
			var selectedOrder = SalesDataGrid.SelectedItem as orders;
			NavigationService.Navigate(new MoreOrderInfoPage(selectedOrder));

		}

		private void reportButton_Click(object sender, RoutedEventArgs e)
		{
			ChoosingDateDialogWindow chooseDate = new ChoosingDateDialogWindow();
			chooseDate.ShowDialog();
			string datte = chooseDate.Value;
			DateTime rtr = Convert.ToDateTime(datte);

            List<orders> selectedTimeOrders = db.context.orders.Where(x => x.order_date == rtr).ToList();
			List<ordered_medecines> selectedOrdrMed = new List<ordered_medecines>();
			List<ordered_medecines> allOrderedMed = db.context.ordered_medecines.ToList();
            Console.WriteLine("общ" + allOrderedMed.Count);
            for (int i= 0; i < allOrderedMed.Count; i++)
			{
				
    //           ordered_medecines drdrd = db.context.ordered_medecines.Where(x => x.id_ordered_medecines == ur).FirstOrDefault();
				//selectedOrdrMed.Add(drdrd);
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
			int[] sumMed = new int[selectedOrdrMed.Count];
			List<int> sumMed2 = new List<int>();
			List<medicines> existingMedicines = db.context.medicines.ToList();
            List<ordered_medecines> selected = new List<ordered_medecines>();
			int count = 0;
			Console.WriteLine(selectedOrdrMed.Count);
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
                            count += Convert.ToInt32(selectedOrdrMed[j].selected_medecines_amount);
                        }
						//Console.WriteLine("j= " + j+ " "+ selectedOrdrMed[i].medicines_id_medicines+ " "+ selectedOrdrMed[j].medicines_id_medicines+ "   "+ count);

                    }
					sumMed2.Add(Convert.ToInt32(selectedOrdrMed[i].medicines_id_medicines));
                    Console.WriteLine("}}" +selectedOrdrMed[i].medicines_id_medicines + "  " + count);
					count = 0;
                }
				
				
				//selected.AddRange( db.context.ordered_medecines.Where(x => x.medicines_id_medicines == i).ToList());
			}
			
			

        

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


        }
	}
}
