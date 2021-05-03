using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for MedicineInformationPreview.xaml
    /// </summary>
    public partial class MedicineInformationPreview : Window
    {
        private Medicine medicine;
        private static MedicineInformationPreview instance = null;

        public static MedicineInformationPreview GetInstance(Medicine medicine)
        {
            if (instance == null)
                instance = new MedicineInformationPreview(medicine);
            return instance;
        }
        private MedicineInformationPreview(Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
            medicineIngredientsTable.DataContext = medicine.Ingredients;
            initData();
        }

        private void initData()
        {
            medicineIdLabel.Content = medicine.Id;
            medicineNameTextBox.Text = medicine.Name;
            medicineReplacmentComboBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
            if (medicine.ReplacementMedicine != null)
                medicineReplacmentComboBox.SelectedItem = medicine.ReplacementMedicine;
            List<String> medicineTypeList = new List<String>();
            String[] typeOfMedicines = { "Rastvor", "Pilula", "Sirup", "Tableta"};
            medicineTypeList.AddRange(typeOfMedicines);
            medicineTypeComboBox.ItemsSource = medicineTypeList;
            if(medicine.Type == TypeOfMedicine.Dilution)
            {
                medicineTypeComboBox.SelectedItem = "Rastvor";
            }
            else if(medicine.Type == TypeOfMedicine.Tablet)
            {
                medicineTypeComboBox.SelectedItem = "Tableta";
            }
            else if (medicine.Type == TypeOfMedicine.Syrup)
            {
                medicineTypeComboBox.SelectedItem = "Sirup";
            }
            else
            {
                medicineTypeComboBox.SelectedItem = "Pilula";
            }

            medicinePurposeTextBox.Text = medicine.Purpose;
            wayOfUseTextBox.Text = medicine.WayOfUse;
            RefreshTable();
        }

        private void RefreshTable()
        {
            medicineIngredientsTable.ItemsSource = null;
            medicineIngredientsTable.ItemsSource = new ObservableCollection<MedicineIngredient>(medicine.Ingredients);
        }

        private void editMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (string.Equals(editMedicine.Content, "IZMENI"))
            {
                medicineNameTextBox.IsEnabled = true;
                medicinePurposeTextBox.IsEnabled = true;
                wayOfUseTextBox.IsEnabled = true;
                medicineReplacmentComboBox.IsEnabled = true;
                medicineTypeComboBox.IsEnabled = true;
                addIngredientButton.IsEnabled = true;
                deleteIngredientButton.IsEnabled = true;
                editMedicine.Content = "ZAVRSI";
            }
            else
            {
                medicine.Name = medicineNameTextBox.Text;
                medicine.ReplacementMedicine = (Medicine)medicineReplacmentComboBox.SelectedItem;
                if(String.Equals(medicineTypeComboBox.SelectedItem.ToString(), "Rastvor"))
                {
                    medicine.Type = TypeOfMedicine.Dilution;
                }
                else if(String.Equals(medicineTypeComboBox.SelectedItem.ToString(), "Tableta"))
                {
                    medicine.Type = TypeOfMedicine.Tablet;
                }
                else if (String.Equals(medicineTypeComboBox.SelectedItem.ToString(), "Sirup"))
                {
                    medicine.Type = TypeOfMedicine.Syrup;
                }
                else
                {
                    medicine.Type = TypeOfMedicine.Pill;
                }
                medicine.Purpose = medicinePurposeTextBox.Text;
                medicine.WayOfUse = wayOfUseTextBox.Text;
                medicineNameTextBox.IsEnabled = false;
                medicinePurposeTextBox.IsEnabled = false;
                wayOfUseTextBox.IsEnabled = false;
                addIngredientButton.IsEnabled = false;
                deleteIngredientButton.IsEnabled = false;
                medicineReplacmentComboBox.IsEnabled = false;
                medicineTypeComboBox.IsEnabled = false;
                editMedicine.Content = "IZMENI";
            }
            
        }

        private void addIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorAddNewIngredientWindow doctorAddNewIngredientWindow = new DoctorAddNewIngredientWindow(medicine);
            doctorAddNewIngredientWindow.ShowDialog();
            RefreshTable();
        }

        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if((MedicineIngredient)medicineIngredientsTable.SelectedItem != null)
                medicine.Ingredients.Remove((MedicineIngredient)medicineIngredientsTable.SelectedItem);
            RefreshTable();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MedicinePreviewWindow.GetInstance().RefreshTable();
            instance = null;
        }
    }
}
