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
    /// Логика взаимодействия для ChoosingDateDialogWindow.xaml
    /// </summary>
    public partial class ChoosingDateDialogWindow : Window
    {
        public ChoosingDateDialogWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; //результат диалогвоого окна при нажатии кнопки
        }

        public string Value
        {
            get { return selectDateDatePicker.Text; }
        }
    }
}
