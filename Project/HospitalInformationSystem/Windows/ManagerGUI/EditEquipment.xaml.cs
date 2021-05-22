﻿using HospitalInformationSystem.Controller;
using HospitalInformationSystem.DTO;
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

namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {

        private static EditEquipment instance = null;
        private Equipment selectedEquipment;
        private int oldQuantity;

        public static EditEquipment getInstance(Equipment equipment)
        {
            if (instance == null)
                instance = new EditEquipment(equipment);
            return instance;
        }
        private EditEquipment(Equipment equipment)
        {
            InitializeComponent();
            this.selectedEquipment = equipment;
            fillControls();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            TypeOfEquipment typeOfEquipment;

            if (typeComboBox.SelectedIndex == 0)
                typeOfEquipment = TypeOfEquipment.Static;
            else if (typeComboBox.SelectedIndex == 1)
                typeOfEquipment = TypeOfEquipment.Dynamic;
            else
                typeOfEquipment = selectedEquipment.Type;

            int newQuantity = int.TryParse(quanitityTextBox.Text, out newQuantity) ? newQuantity : 0;
            string description = descriptionTextBox.Text;

            if (string.Compare(nameTextBox.Text, "") == 0)
                MessageBox.Show("Polje za unos naziva ne može biti prazno!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (newQuantity == 0 || newQuantity < 0)
                MessageBox.Show("Pogrešan unos količine!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                selectedEquipment.UpdateProperties(new EquipmentDTO(name, typeOfEquipment, description, newQuantity, oldQuantity));
                RoomController.GetInstance().GetMagacine().Equipment[selectedEquipment.Id] = newQuantity;
                //promeniti u prostoriji id opreme
                ManagerMainWindow.getInstance().equipmentTable.refreshTable();

                this.Close();
                MessageBox.Show("Informacije o opremi su sada izmenjene.", "Izmena prostorije", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void fillControls()
        {
            Room magacine = RoomController.GetInstance().GetMagacine();
            nameTextBox.Text = selectedEquipment.Name;
            quanitityTextBox.Text = magacine.Equipment[selectedEquipment.Id].ToString();
            descriptionTextBox.Text = selectedEquipment.Description;
            loadTypeComboBox();
            oldQuantity = (int)magacine.Equipment[selectedEquipment.Id];
        }

        private void loadTypeComboBox()
        {
            var list = new List<String>();

            list.Add("Statička");
            list.Add("Dinamička");

            typeComboBox.ItemsSource = list;

            if (selectedEquipment.Type == TypeOfEquipment.Static)
                typeComboBox.SelectedIndex = 0;
            else
                typeComboBox.SelectedIndex = 1;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        public void checkControls()
        {
            int quantityInMagacine = int.TryParse(quanitityTextBox.Text, out quantityInMagacine) ? quantityInMagacine : 0;

            if (nameTextBox.Text != selectedEquipment.Name || (string)typeComboBox.SelectedItem != selectedEquipment.GetStringType || /*quantityInMagacine != selectedEquipment.QuantityInMagacine ||*/ descriptionTextBox.Text != selectedEquipment.Description)
                changeButton.IsEnabled = true;
            else
                changeButton.IsEnabled = false;
        }
        private void nameTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkControls();
        }

        private void quanitityTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }

        private void descriptionTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkControls();
        }
    }
}
