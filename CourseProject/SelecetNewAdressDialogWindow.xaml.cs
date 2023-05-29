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
using System.Windows.Shapes;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для SelecetNewAdressDialogWindow.xaml
    /// </summary>
    public partial class SelecetNewAdressDialogWindow : Window
    {
        Core db = new Core();
       
        public SelecetNewAdressDialogWindow()
        {
            InitializeComponent();
            adressComboBox.ItemsSource = db.context.adresses.Select(p=>p.adress).ToList();

            
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;

           

        }

        public string Value
        {
            get { return adressComboBox.SelectedItem.ToString(); }
        }
    }
}
