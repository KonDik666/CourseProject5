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
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для ChoosingDateIntervalDialogWindow.xaml
    /// </summary>
    public partial class ChoosingDateIntervalDialogWindow : Window
    {
        public ChoosingDateIntervalDialogWindow()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToDateTime(selectFirstDateDatePicker.Text)< Convert.ToDateTime(selectSecondDateDatePicker.Text))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Первичная дата не должна превышать вторичную");
                this.DialogResult = false;
            }
           
        }

        public string Value1
        {
            get { return selectFirstDateDatePicker.Text; }
        }
        public string Value2
        {
            get { return selectSecondDateDatePicker.Text; }
        }
    }
}
