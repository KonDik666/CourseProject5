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
using StringCheckLibrary;
using System.Data.Entity.Migrations;

namespace CourseProject.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : Page
    {
        Core db = new Core();
      
        public int addOrEdit = 0;    //add=0, edit=1
        public user currentUser = new user();
        public NewUserPage(user selectedUser)
        {

            InitializeComponent();

            if (selectedUser != null)                        //получение информации о выбранном для редактирвоания пользователе(если такой имеется)
            {
                nameTextBox.Text = selectedUser.name;
                surnameTextBox.Text = selectedUser.surname;
                patronymicTextBox.Text = selectedUser.patronymic;
                roleComboBox.SelectedItem = selectedUser.role;
                phoneTextBox.Text = selectedUser.phone_number;
                emailTextBox.Text = selectedUser.email;
                loginTextBox.Text = selectedUser.login;
                passwordTextBox.Text = selectedUser.password;
                addOrEdit = 1;
                currentUser = selectedUser;
            }
            
            
            roleComboBox.ItemsSource = new string[] { "stuff", "admin", "owner" };

            
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int checking = 0;
            if (CheckClass.CheckNameString(nameTextBox.Text)==false)     //проверка вводимой информации с помощью библиотеки StringCheckLibrary
            {
                nameTextBox.Text = "";                               
                MessageBox.Show("Неправильно введено имя");
                
            }
            else
            {
                checking += 1;            //переменная checking для проверки правильности ввода(при соблюдении условия она повышается на 1)
            }

            if (CheckClass.CheckNameString(surnameTextBox.Text) == false)
            {
                surnameTextBox.Text = "";
                MessageBox.Show("Неправильно введена фамилия");

            }
            else
            {
                checking += 1;
            }

            if (CheckClass.CheckNameString(patronymicTextBox.Text) == false)
            {
                patronymicTextBox.Text = "";
                MessageBox.Show("Неправильно введено отчество");

            }
            else
            {
                checking += 1;
            }

            if(roleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать роль пользователя");
            }
            else
            {
                checking += 1;
            }
            if (CheckClass.CheckPhoneNumberString(phoneTextBox.Text) == false)
            {
                phoneTextBox.Text = "";
                MessageBox.Show("Неправильно введен номер телефона");
            }
            else
            {
                checking += 1;
            }
            if (CheckClass.CheckEmailString(emailTextBox.Text) == false)
            {
                emailTextBox.Text = "";
                MessageBox.Show("Неправильно введена электронная почта");
            }
            else
            {
                checking += 1;
            }
            if(CheckClass.CheckLoginString(loginTextBox.Text) == false)
            {
                loginTextBox.Text = "";
                MessageBox.Show("Неправильно введен логин");
            }
            else
            {
                checking += 1;
            }
            if (CheckClass.CheckPasswordString(passwordTextBox.Text)==false)
            {
                passwordTextBox.Text = "";
                MessageBox.Show("Неправильно введен пароль(он должен содержать как минимум одну латинскую букву, цифру, символ и иметь длину не менее 8)");
            }
            else
            {
                checking += 1;
            }

            //Добавление нового пользователя в базу данных
            if (checking == 8 && addOrEdit==0)
            {
                try
                {
                    user people = new user();

                    people.name = nameTextBox.Text;
                    people.surname = surnameTextBox.Text;
                    people.patronymic = patronymicTextBox.Text;
                    people.role = roleComboBox.SelectedItem.ToString();
                    people.phone_number = phoneTextBox.Text;
                    people.email = emailTextBox.Text;
                    people.login = loginTextBox.Text;
                    people.password = passwordTextBox.Text;

                    db.context.user.Add(people);
                    db.context.SaveChanges();
                    MessageBox.Show("Пользователь был успешно добавлен в базу");
                    NavigationService.GoBack();
                    

                }
                catch
                {
                    MessageBox.Show("Ошибка при добавлении информации в базу данных");
                }



            }

            //Редактирование существующего пользователя в базе данных
            else if (checking == 8 && addOrEdit == 1)
            {
                    
                    currentUser.name = nameTextBox.Text;
                    currentUser.surname = surnameTextBox.Text;
                    currentUser.patronymic = patronymicTextBox.Text;
                    currentUser.role = roleComboBox.SelectedItem.ToString();
                    currentUser.phone_number = phoneTextBox.Text;
                    currentUser.email = emailTextBox.Text;
                    currentUser.login = loginTextBox.Text;
                    currentUser.password = passwordTextBox.Text;

                    db.context.user.AddOrUpdate(currentUser);
                    db.context.SaveChanges();
                
                    MessageBox.Show("Пользователь был успешно обновлен в базе");
                    NavigationService.GoBack();
                 

              
            }




        }
    }
}
