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
    /// Логика взаимодействия для NewProviderPage.xaml
    /// </summary>
    public partial class NewProviderPage : Page
    {
        Core db = new Core();
        providers currentProviders = new providers();
        public int addOrEdit = 0;  //определение будет ли пользователь добавлять или редактирвоать с помощью перемнной addOrEdit
        public NewProviderPage(providers selectedProvider)  
        {
            InitializeComponent();
            currentProviders = selectedProvider;

            if(currentProviders != null)  //если входное значение страницы не пустое то заполнение текстблоков данными выбранного поставщика для редактирвоания
            {
                addOrEdit = 1;
                newProviderNameTextBox.Text = selectedProvider.provider_name;  
                newProviderDateTextBox.Text = selectedProvider.supplies_date;
            }
            
            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

            if (addOrEdit == 0)  //добавление нового поставщика в базу
            {
                providers newProvider = new providers();
                newProvider.provider_name = newProviderNameTextBox.Text;
                newProvider.supplies_date = newProviderDateTextBox.Text;
                db.context.providers.Add(newProvider);
                db.context.SaveChanges();
                MessageBox.Show("Поставщик был успешно добавлен в базу");
                NavigationService.GoBack();
            }
            else if(addOrEdit == 1) //редактирвоание существующего поставщика в базе
            {
                currentProviders.provider_name = newProviderNameTextBox.Text;
                currentProviders.supplies_date = newProviderDateTextBox.Text;
                try
                {
                    db.context.providers.AddOrUpdate(currentProviders);
                    db.context.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
                MessageBox.Show("Поставщик был успешно обновлен в базе");
                NavigationService.GoBack();
            }
           
        }
    }
}
