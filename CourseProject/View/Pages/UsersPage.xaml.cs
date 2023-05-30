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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        Core db = new Core();
       
        public UsersPage()
        {
            InitializeComponent();

            if (App.CurrentUser.role == "stuff")
            {
                addUserButton.IsEnabled = false;  //данные кнопки не доступны для пользоватлеей "stuff"
                removeUserButton.IsEnabled = false;
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            usersInfoGrid.ItemsSource = db.context.user.ToList();  //формирвоание датагрид на оснвое таблицы user
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewUserPage(null));  //перенаправление на станницу добавления или редактирвоания нового пользователя
        }

        private void removeUserButton_Click(object sender, RoutedEventArgs e) //удалене пользовтаеля
        {
            if (MessageBox.Show("Вы действительно хотите удалить данного пользователя?","Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                var SelectedUser = usersInfoGrid.SelectedItem as user;
                db.context.user.Remove(SelectedUser);
                db.context.SaveChanges();

                usersInfoGrid.ItemsSource = db.context.user.ToList();
                MessageBox.Show("Пользователь успешно удален");
            }
        }

        private void changeUserInfoButton_Click(object sender, RoutedEventArgs e)
        {

            var SelectedUser = usersInfoGrid.SelectedItem as user;  //изменение информации о пользлователе
            if (App.CurrentUser.role == "stuff")
            {
               
                if (SelectedUser.id_user == App.CurrentUser.id_user)
                {
                    NavigationService.Navigate(new NewUserPage(SelectedUser));
                }
                else
                {
                    MessageBox.Show("Пользователи со статусом <stuff> не могут редактировать других пользователей");
                }
            }
            else
            {
                NavigationService.Navigate(new NewUserPage(SelectedUser));
            }
           

        }
    }
}
