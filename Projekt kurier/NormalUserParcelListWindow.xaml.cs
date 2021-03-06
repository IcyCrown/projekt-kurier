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
using System.Collections;
using System.Globalization;
using System.Windows.Navigation;
using System.ComponentModel;


namespace Projekt_kurier
{
    /// <summary>
    /// Interaction logic for UserParcelListWindow.xaml
    /// </summary>
    public partial class NormalUserParcelListWindow : Window
    {
        public NormalUserParcelListWindow()
        {
            InitializeComponent();
            checkButton.IsEnabled = false;
            editButton.IsEnabled = false;
            removeButton.IsEnabled = false;
            userListBox.ItemsSource = DB.PackagesList;
        }

        private void userListBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            userListBox.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            View.Filter = delegate (object item)
            {
                Package package = item as Package;
                if (package != null)
                {
                    return (package.Sender.Login == ((NormalUserWindow)Owner).CurrentUser.Login);
                }
                return false;
            };

            //try
            //{
            //    userListBox.ItemsSource = from package in DB.Instance.Packages
            //                              where package.Sender.Login == ((NormalUserWindow)Owner).CurrentUser.Login
            //                              select package;
            //}
            //catch (NullReferenceException) { }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            DB.PackagesList.Remove((Package)userListBox.SelectedItem);
            userListBox.Items.Refresh();
            View.Refresh();
        }

        private void Check_Status(object sender, RoutedEventArgs e)
        {
            SettingSource settings = new SettingSource(DB.PackagesList[userListBox.SelectedIndex].State);
            ParcelStatusInfoWindow win = new ParcelStatusInfoWindow(settings);
            win.Owner = this;
            win.ShowDialog();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditParcelDataWindow epdw = new EditParcelDataWindow();
            epdw.Owner = this;
            epdw.DataContext = userListBox.SelectedItem;
            epdw.ShowDialog();
        }

        public ListCollectionView View
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(DB.Instance.Packages);
            }
        }
        private void ParcelStatusSort_Selected(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("State", ListSortDirection.Descending));
            userListBox.Items.Refresh();
        }
        private void SortCancel_Selected(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
        }
        private void IDSearch_Clicked(object sender, RoutedEventArgs e)
        {
            decimal id;
            if (Decimal.TryParse(IDSearch.Text, out id))
            {
                View.Filter = delegate (object item)
                {
                    Package package = item as Package;
                    if (package != null)
                    {
                        return (userListBox.Items.IndexOf(package) == id);
                    }
                    return false;
                };
            }
            userListBox.Items.Refresh();
        }
        private void ParcelIDSort_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void userListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userListBox.SelectedIndex>=0)
            {
                checkButton.IsEnabled = true;
                editButton.IsEnabled = true;
                removeButton.IsEnabled = true;
            }
            else
            {
                checkButton.IsEnabled = false;
                editButton.IsEnabled = false;
                removeButton.IsEnabled = false;
            }
        }
    }
}
