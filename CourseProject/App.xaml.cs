using CourseProject.View.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        //текущий пользователь
        public static user CurrentUser { get; set; } = null;

        public static user CurrentUser2 { get; set; } = null;

        //текущее лекарство
        public static medicines currentMedPreparate { get; set; } = null;
        //текщий адресс
        public static adresses currentAdressName { get; set; } = null;

        public static List<medicines> selectedToOrderMedecines = new List<medicines>(); 

        public static int Identificator;

        public static medicines selectedCurrentMedecine { get; set; } = null;


    }
}
