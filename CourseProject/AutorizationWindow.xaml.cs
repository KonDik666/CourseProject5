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
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        Core db = new Core();
        List<user> arrayUsers;
        public AutorizationWindow()
        {
            InitializeComponent();
            arrayUsers = db.context.user.ToList();

        }

        private void autorizationButton_Click(object sender, RoutedEventArgs e)
        {
            //авторизация
            var countRecord = arrayUsers.Where(x => x.login == LoginTextBlock.Text && x.password == PassPasswordBox.Password).FirstOrDefault();
            if (countRecord != null)
            {
                App.CurrentUser = countRecord;
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ввели некорректные данные");
            }

            
        }
    }
}
