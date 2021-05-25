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
        private Medicine _medicineToPreview;
        private static MedicineInformationPreview instance = null;
        public static MedicineInformationPreview GetInstance(Medicine medicineToPreview)
        {
            if (instance == null)
                instance = new MedicineInformationPreview(medicineToPreview);
            return instance;
        }
        private MedicineInformationPreview(Medicine medicineToPreview)
        {
            InitializeComponent();
            this._medicineToPreview = medicineToPreview;
            medicineIngredientsTable.DataContext = _medicineToPreview.Ingredients;
            InitData();
            InitMedicineTypeComboBox();
        }
        private void InitMedicineTypeComboBox()
        {
            List<String> medicineTypeList = new List<String>();
            String[] typeOfMedicines = { "Rastvor", "Pilula", "Sirup", "Tableta" };
            medicineTypeList.AddRange(typeOfMedicines);
            medicineTypeComboBox.ItemsSource = medicineTypeList;
            InitSelectedItemInMedicineTypeComboBox();
        }
        private void InitSelectedItemInMedicineTypeComboBox()
        {
            switch(_medicineToPreview.Type)
            {
                case TypeOfMedicine.Dilution:
                    medicineTypeComboBox.SelectedItem = "Rastvor";
                    break;
                case TypeOfMedicine.Tablet:
                    medicineTypeComboBox.SelectedItem = "Tableta";
                    break;
                case TypeOfMedicine.Syrup:
                    medicineTypeComboBox.SelectedItem = "Sirup";
                    break;
                default:
                    medicineTypeComboBox.SelectedItem = "Pilula";
                    break;
            }
        }
        private void InitData()
        {
            medicineIdLabel.Content = _medicineToPreview.Id;
            medicineNameTextBox.Text = _medicineToPreview.Name;
            medicineReplacmentComboBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
            if (_medicineToPreview.ReplacementMedicine != null)
                medicineReplacmentComboBox.SelectedItem = _medicineToPreview.ReplacementMedicine;
            medicinePurposeTextBox.Text = _medicineToPreview.Purpose;
            wayOfUseTextBox.Text = _medicineToPreview.WayOfUse;
            RefreshTable();
        }
        private void RefreshTable()
        {
            medicineIngredientsTable.ItemsSource = null;
            medicineIngredientsTable.ItemsSource = new ObservableCollection<MedicineIngredient>(_medicineToPreview.Ingredients);
        }
        private void editMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (string.Equals(editMedicine.Content, "IZMENI"))
                EnableMedicineEditing();
            else
                FinishEditingMedicine();
        }
        private void EnableMedicineEditing()
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
        private void FinishEditingMedicine()
        {
            _medicineToPreview.Name = medicineNameTextBox.Text;
            _medicineToPreview.ReplacementMedicine = (Medicine)medicineReplacmentComboBox.SelectedItem;
            ChangeMedicineType();
            _medicineToPreview.Purpose = medicinePurposeTextBox.Text;
            _medicineToPreview.WayOfUse = wayOfUseTextBox.Text;
            DisableEditing();
        }
        private void ChangeMedicineType()
        {
            switch(medicineTypeComboBox.SelectedItem.ToString())
            {
                case "Rastvor":
                    _medicineToPreview.Type = TypeOfMedicine.Dilution;
                    break;
                case "Tableta":
                    _medicineToPreview.Type = TypeOfMedicine.Tablet;
                    break;
                case "Sirup":
                    _medicineToPreview.Type = TypeOfMedicine.Syrup;
                    break;
                default:
                    _medicineToPreview.Type = TypeOfMedicine.Pill;
                    break;
            }
        }
        private void DisableEditing()
        {
            medicineNameTextBox.IsEnabled = false;
            medicinePurposeTextBox.IsEnabled = false;
            wayOfUseTextBox.IsEnabled = false;
            addIngredientButton.IsEnabled = false;
            deleteIngredientButton.IsEnabled = false;
            medicineReplacmentComboBox.IsEnabled = false;
            medicineTypeComboBox.IsEnabled = false;
            editMedicine.Content = "IZMENI";
        }
        private void addIngredientButton_Click(object sender, RoutedEventArgs e) => 
            DoctorAddNewIngredientWindow.GetInstance(_medicineToPreview).ShowDialog();
        private void deleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if((MedicineIngredient)medicineIngredientsTable.SelectedItem != null)
                _medicineToPreview.Ingredients.Remove((MedicineIngredient)medicineIngredientsTable.SelectedItem);
            RefreshTable();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MedicinePreviewWindow.GetInstance().RefreshTable();
            instance = null;
        }
        private void addCommentButton_Click(object sender, RoutedEventArgs e) => 
            AddCommentOnMedicineWindow.GetInstance(_medicineToPreview).ShowDialog();
    }
}
