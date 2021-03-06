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

namespace Projekt_kurier
{
    /// <summary>
    /// Interaction logic for EditCourierDataWindow.xaml
    /// </summary>
    public partial class EditCourierDataWindow : Window
    {
        public Courier css { get; set; }
        public EditCourierDataWindow()
        {
            InitializeComponent();
        }

        private void Save_Click_Button(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == String.Empty || SurnameTextBox.Text == String.Empty || LoginTextBox.Text == String.Empty || PasswordTextBox.Text == String.Empty)
            {
                MessageBox.Show("Uzupełnij puste pola!");
                return;
            }
            Courier us = null;
            try
            {
                us = DB.CouriersList.Where(u => u.Login == LoginTextBox.Text).Single();
            }
            catch (Exception) { }
            if (us != null && css.Login !=LoginTextBox.Text)
            {
                MessageBox.Show("Podany login jest zajety!");
                return;
            }
            BindingExpression binding = NameTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            binding = SurnameTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            binding = PasswordTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            binding = LoginTextBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            MessageBox.Show("Aktualizacja danych powiodła się!");
        }

        private void Exit_Click_Button(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
