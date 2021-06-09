using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using HospitalInformationSystem.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
namespace HospitalInformationSystem.Windows.ManagerGUI
{
    /// <summary>
    /// Interaction logic for EditMedicineWindow.xaml
    /// </summary>
    public partial class EditMedicineWindow : Window
    {
        private Medicine medicineForEdit;
        private List<MedicineIngredient> medicineIngredientList = new List<MedicineIngredient>(); 
        private static EditMedicineWindow instance = null;
        public static EditMedicineWindow GetInstance(Medicine medicineForEdit)
        {
            if (instance == null)
                instance = new EditMedicineWindow(medicineForEdit);
            return instance;
        }
        private EditMedicineWindow(Medicine medicineForEdit)
        {
            InitializeComponent();
            this.medicineForEdit = medicineForEdit;
            LoadComboBoxes();
            LoadIngredientsListBox();
            LoadSelectedMedicineAttributes();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
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
            replacementMedicineComboBox.ItemsSource = Try();
        }
        private ObservableCollection<string> Try()
        {
            ObservableCollection<string> replacementMedicines = new ObservableCollection<string>();
            foreach (Medicine medicine in MedicineController.GetInstance().GetAllMedicines())
            {
                replacementMedicines.Add(medicine.Name);
            }
            return replacementMedicines;
        }
        private void LoadIngredientsListBox()
        {
            foreach (MedicineIngredient mi in medicineForEdit.Ingredients)
                medicineIngredientList.Add(mi);
        }
        private void RefreshIngredientsListBox()
        {
            ingredientsListBox.ItemsSource = null;
            ingredientsListBox.ItemsSource =  medicineIngredientList;
        }
        private void LoadSelectedMedicineAttributes()
        {
            idTextBox.Text = medicineForEdit.Id.ToString();
            nameTextBox.Text = medicineForEdit.Name;
            SelectTypeInTypeOfMedicineComboBox();
            purposeTextBoxt.Text = medicineForEdit.Purpose;
            useTextBox.Text = medicineForEdit.WayOfUse;
            nameOfReplacementMedicineTextBlock.Text = medicineForEdit.NameOfReplacementMedicine;
            RefreshIngredientsListBox();
        }
        private void SelectTypeInTypeOfMedicineComboBox()
        {
            if (medicineForEdit.Type == TypeOfMedicine.Dilution)
                typeComboBox.SelectedIndex = 0;
            else if (medicineForEdit.Type == TypeOfMedicine.Syrup)
                typeComboBox.SelectedIndex = 1;
            else if (medicineForEdit.Type == TypeOfMedicine.Tablet)
                typeComboBox.SelectedIndex = 2;
            else
                typeComboBox.SelectedIndex = 3;
        }
        private void addNewIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            NewIngredientWindow.GetInstance(medicineIngredientList).ShowDialog();
            RefreshIngredientsListBox();
        }
        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            medicineIngredientList.Remove((MedicineIngredient)ingredientsListBox.SelectedItem);
            RefreshIngredientsListBox();
        }
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckControlsInputCorrection())
            {
                medicineForEdit.UpdateMedicine(new Medicine(
                    int.Parse(idTextBox.Text), nameTextBox.Text, LoadMedicineType(),
                    purposeTextBoxt.Text, useTextBox.Text, LoadReplacementMedicine(), medicineIngredientList));
                this.Close();
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
        private string LoadReplacementMedicine()
        {
            if (replacementMedicineComboBox.SelectedItem != null)
                return (string)replacementMedicineComboBox.SelectedItem;
            return medicineForEdit.ReplacementMedicine;
        }
        private bool CheckControlsInputCorrection()
        {
            if (!IdTextBoxCorrection())
                return CreateErrorMessageBox("Šifra mora biti ceo broj!");
            if (CheckTheExistenceOfId() && medicineForEdit.Id != int.Parse(idTextBox.Text))
                return CreateErrorMessageBox("U sistemu postoji lek sa ovom šifrom!");
            if (!NameTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos imena ne može biti prazno!");
            if (!PurposeTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos namene ne može biti prazno!");
            if (!WayOfUseTextBoxCorrection())
                return CreateErrorMessageBox("Polje za unos načina upotrebe ne može biti prazno!");
            if (CheckTheExistenceOfAnIngredient() == 0)
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
            return !(MedicineController.GetInstance().FindMedicineById(int.Parse(idTextBox.Text)) == null);
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
