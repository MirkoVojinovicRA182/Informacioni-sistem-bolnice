using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using HospitalInformationSystem.Utility;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    public partial class NewMedicineWindow : MetroWindow
    {
        private static NewMedicineWindow instance = null;
        private List<MedicineIngredient> medicineIngredientList = new List<MedicineIngredient>();
        public static NewMedicineWindow GetInstance()
        {
            if (instance == null)
                instance = new NewMedicineWindow();
            return instance;
        }
        private NewMedicineWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            LoadTypeOfMedicineComboBox();
            LoadReplacementMedicinesComboBox();
        }
        private void LoadTypeOfMedicineComboBox()
        {
            string[] typeOfMedicineList = { Constants.DILUTION, Constants.SYRUP, Constants.PILL, Constants.TABLET };
            typeComboBox.ItemsSource = new List<string>(typeOfMedicineList);
        }
        private void LoadReplacementMedicinesComboBox()
        {
            replacementMedicineComboBox.ItemsSource = null;
            replacementMedicineComboBox.ItemsSource = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckControlsInputCorrection())
            {
                MedicineController.GetInstance().AddMedicine(new Medicine(int.Parse(idTextBox.Text), nameTextBox.Text, LoadMedicineType(), purposeTextBoxt.Text, useTextBox.Text, LoadReplacementMedicine(), medicineIngredientList));
                ManagerMainWindow.getInstance().medicineTableUserControl.RefreshTable();
                this.Close();
                MessageBox.Show("Unet je novi lek u sistem.", "Novi lek", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private TypeOfMedicine LoadMedicineType()
        {
            if (((string)typeComboBox.SelectedItem).CompareTo(Constants.DILUTION) == 0)
                return TypeOfMedicine.Dilution;
            else if (((string)typeComboBox.SelectedItem).CompareTo(Constants.SYRUP) == 0)
                return TypeOfMedicine.Syrup;
            else if (((string)typeComboBox.SelectedItem).CompareTo(Constants.TABLET) == 0)
                return TypeOfMedicine.Tablet;
            else
                return TypeOfMedicine.Pill;
        }
        private Medicine LoadReplacementMedicine()
        {
            if (replacementMedicineComboBox.SelectedItem != null)
                return (Medicine)replacementMedicineComboBox.SelectedItem;
            return null;
        }
        private void addNewIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            NewIngredientWindow.GetInstance(medicineIngredientList).ShowDialog();
            RefreshIngredientsListBox();
        }
        private void RefreshIngredientsListBox()
        {
            ingredientsListBox.ItemsSource = null;
            ingredientsListBox.ItemsSource = medicineIngredientList;
        }

        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            medicineIngredientList.Remove((MedicineIngredient)ingredientsListBox.SelectedItem);
            RefreshIngredientsListBox();
        }
        private bool CheckControlsInputCorrection()
        {
            if (!IdTextBoxCorrection())
                return CreateErrorMessageBox("Šifra mora biti ceo broj!");
            if (CheckTheExistenceOfId())
                return CreateErrorMessageBox("U sistemu postoji lek sa ovom šifrom!");
            if (!NameTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos imena ne može biti prazno!");
            if (!PurposeTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos namene ne može biti prazno!");
            if (!WayOfUseTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos načina upotrebe ne može biti prazno!");
            if(CheckTheExistenceOfAnIngredient() == 0)
                return CreateErrorMessageBox("Morate uneti bar jedan sastojak!");
            return true;
        }
        private bool NameTextBoxCorrection()
        {
            return !(nameTextBox.Text == "");
        }
        private bool IdTextBoxCorrection()
        {
            int tryParseStringToInt = int.TryParse(idTextBox.Text, out tryParseStringToInt) ? tryParseStringToInt : 0;
            return !(tryParseStringToInt == 0);
        }
        private bool CheckTheExistenceOfId()
        {
            return !(MedicineController.GetInstance().FindById(int.Parse(idTextBox.Text)) == null);
        }
        private bool PurposeTextBoxCorrection()
        {
            return !(purposeTextBoxt.Text == "");
        }
        private bool WayOfUseTextBoxCorrection()
        {
            return !(useTextBox.Text == "");
        }
        private int CheckTheExistenceOfAnIngredient()
        {
            return ingredientsListBox.Items.Count;
        }
        private bool CreateErrorMessageBox(string errorMessage)
        {
            MessageBox.Show(errorMessage, Constants.ERROR_MESSAGE_BOX_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
