using CourseProject.View.Model;
using CourseProject.View.Pages;
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


namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Core db = new Core();
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
            int index = 1;
            App.currentAdressName = db.context.adresses.Where(x=>x.is_last_selected==index).FirstOrDefault();

        }


        //метка с информацией об авторизованном пользователе
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content;
            

            if (page is MainPage)
            {
                userLoggedLabel.Content = "Выполнен вход как " + App.CurrentUser.surname +" "+ App.CurrentUser.name + " " + App.CurrentUser.patronymic + ", " + "Роль: "+ App.CurrentUser.role;
                Exit.Text = "Выйти";
                MainHeader.Text = "Выберите интересующий вас раздел";
                locationLabel.Content = "Текущий адрес- "+ App.currentAdressName.adress;
                changeLocationButton.Visibility = Visibility.Visible;



            }
            else
            {
                changeLocationButton.Visibility = Visibility.Collapsed;
            }
           
            if(page is MedecinesPage)
            {
                Exit.Text = "Hазад";
                MainHeader.Text = "Лекарственные препараты(по АТХ классификации)";
            }
            if(page is MedecineInfoPage)
            {
                Exit.Text = "Назaд";
                MainHeader.Text = "Подробнее о препарате";
            }
            if (page is UsersPage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Список пользователей";
            }

            if (page is NewUserPage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Добавьте нового пользовавтеля";
            }

            if (page is VaultsListsPage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Добавьте нового пользовавтеля";
            }
            if(page is MedecinesAmount)
            {
                Exit.Text = "Нaзад";
                MainHeader.Text = "Информация о колличестве препарата";
            }
            if(page is NewMedecinePage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Добавьте или обновите лекарство";
            }
            if(page is ProvidersInfoPage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Список поставщиков";
            }
            if (page is PurchasePage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Оформление покупки";
            }
            if(page is SalesPage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Информация о продажах";
            }
            if (page is MoreOrderInfoPage)
            {
                Exit.Text = "Назад";
                MainHeader.Text = "Подробная нформация о продажах";
            }


        }


        //возврат на пред. старницу
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Convert.ToString(Exit.Text) == "Выйти")
            {
                AutorizationWindow aw = new AutorizationWindow();
                aw.Show();
                this.Close();
            }
            else if(Convert.ToString(Exit.Text) == "Нaзад")
            {
                MainFrame.Navigate(new MedecineInfoPage(App.currentMedPreparate));
            }
            else if (Convert.ToString(Exit.Text) == "Назaд")
            {
                MainFrame.Navigate(new MedecinesPage());
            }
            else if (Convert.ToString(Exit.Text) == "Hазад")
            {
                MainFrame.Navigate(new MainPage());
            }
            else
            {
                MainFrame.GoBack();
            }

            
        }

        private void changeLocationButton_Click(object sender, RoutedEventArgs e)
        {
            SelecetNewAdressDialogWindow adressChange = new SelecetNewAdressDialogWindow();
            adressChange.ShowDialog();

            adresses changingOldAdress = db.context.adresses.Where(x => x.is_last_selected == App.currentAdressName.is_last_selected).FirstOrDefault();
            changingOldAdress.is_last_selected = 0;
            db.context.SaveChanges();
            string bebe = adressChange.Value;
            adresses changingNewAdress = db.context.adresses.Where(x => x.adress == bebe).FirstOrDefault();
            changingNewAdress.is_last_selected = 1;
            db.context.SaveChanges();
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
            
        }
    }
}
