﻿using HospitalInformationSystem.Controller;
using Model;
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

namespace HospitalInformationSystem.Windows.Manager
{
    /// <summary>
    /// Interaction logic for NewEquipment.xaml
    /// </summary>
    public partial class NewEquipment : Window
    {

        private static NewEquipment instance = null;
        private int selectedEquipment;

        public static NewEquipment getInstance(int selectedEquipment)
        {
            if (instance == null)
                instance = new NewEquipment(selectedEquipment);
            return instance;
        }
        private NewEquipment(int selectedEquipment)
        {
            InitializeComponent();

            this.selectedEquipment = selectedEquipment;
        }

        private void newEquipmentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string id = idTextBox.Text;
            string name = nameTextBox.Text;
            int quantity = int.TryParse(quanitityTextBox.Text, out quantity) ? quantity : 0;
            string description = decriptionTextBox.Text;
            TypeOfEquipment typeOfEquipment;

            if (selectedEquipment == 1)
                typeOfEquipment = TypeOfEquipment.Static;
            else
                typeOfEquipment = TypeOfEquipment.Dynamic;

            if(string.Compare(id, "") == 0)
                MessageBox.Show("Polje za unos šifre ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.Compare(nameTextBox.Text, "") == 0)
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (quantity == 0)
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {

                Equipment equipment = new Equipment(id, name, typeOfEquipment, quantity, description);

                EquipmentController.getInstance().addNewEquipment(equipment);

                ManagerMainWindow.getInstance().equipmentTable.refreshTable();
                ManagerMainWindow.getInstance().dynamicEquipmentTable.refreshTable();

                //obavestavanje korisnika o uspesno unetoj opremi
                MessageBox.Show("Uneta je nova oprema u sistem.", "Nova prostorija", MessageBoxButton.OK, MessageBoxImage.Information);

                //brisanje polja nakon uspesnog unosa

                idTextBox.Clear();
                nameTextBox.Clear();
                quanitityTextBox.Clear();
            }
            
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            instance = null;
        }
    }
}
