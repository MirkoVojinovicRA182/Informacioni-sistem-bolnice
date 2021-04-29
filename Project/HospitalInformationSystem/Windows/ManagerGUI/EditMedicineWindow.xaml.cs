using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            FillWpfControls();
        }
        private void LoadComboBoxes()
        {
            List<String> typeOfMedicineList = new List<String>();
            typeOfMedicineList.Add("Rastvor");
            typeOfMedicineList.Add("Sirup");
            typeOfMedicineList.Add("Tableta");
            typeOfMedicineList.Add("Pilula");
            typeComboBox.ItemsSource = typeOfMedicineList;

            ObservableCollection<Medicine> replacementMedicinesList = new ObservableCollection<Medicine>(MedicineController.GetInstance().GetAllMedicines());
            replacementMedicineComboBox.ItemsSource = null;
            replacementMedicineComboBox.ItemsSource = replacementMedicinesList;
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
        private void FillWpfControls()
        {
            idTextBox.Text = medicineForEdit.Id.ToString();
            nameTextBox.Text = medicineForEdit.Name;
            SelectTypeInTypeOfMedicineComboBox();
            purposeTextBoxt.Text = medicineForEdit.Purpose;
            useTextBox.Text = medicineForEdit.WayOfUse;
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            MedicineController.GetInstance().ChangeMedicine(medicineForEdit, new Medicine(
                int.Parse(idTextBox.Text), nameTextBox.Text, TypeOfMedicine.Dilution,
                purposeTextBoxt.Text, useTextBox.Text, null, medicineIngredientList));
            this.Close();
        }
    }
}
