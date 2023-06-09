﻿using System;
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
    /// Логика взаимодействия для MedecinesNewAmountDialogWindow.xaml
    /// </summary>
    public partial class MedecinesNewAmountDialogWindow : Window
    {
        public MedecinesNewAmountDialogWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Value
        {
            get { return valueBox.Text; } //результат диалогвоого окна при нажатии кнопки
        }
    }
}
