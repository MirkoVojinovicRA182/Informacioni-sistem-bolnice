using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
            medicineNameTextBox.Text = _medicineToPreview.Name;
            medicineReplecmentListBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
            if (_medicineToPreview.ReplacementMedicine != null)
                medicineReplecmentListBox.SelectedItem = _medicineToPreview.ReplacementMedicine;
            medicinePurposeTextBox.Text = _medicineToPreview.Purpose;
            wayOfUseTextBox.Text = _medicineToPreview.WayOfUse;
            RefreshTable();
        }
        public void RefreshTable()
        {
            medicineIngredientsTable.ItemsSource = null;
            medicineIngredientsTable.ItemsSource = new ObservableCollection<MedicineIngredient>(_medicineToPreview.Ingredients);
        }
        private void EnableMedicineEditing()
        {
            medicineNameTextBox.IsEnabled = true;
            medicinePurposeTextBox.IsEnabled = true;
            wayOfUseTextBox.IsEnabled = true;
            medicineReplecmentListBox.IsEnabled = true;
            medicineTypeComboBox.IsEnabled = true;
        }
        private void FinishEditingMedicine()
        {
            _medicineToPreview.Name = medicineNameTextBox.Text;
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
            medicineReplecmentListBox.IsEnabled = false;
            medicineTypeComboBox.IsEnabled = false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MedicinePreviewWindow.GetInstance().RefreshTable();
            instance = null;
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.E))
            {
                if (!medicineNameTextBox.IsEnabled)
                {
                    EnableMedicineEditing();
                }
                else
                    FinishEditingMedicine();
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.K))
                AddCommentOnMedicineWindow.GetInstance(_medicineToPreview).ShowDialog();
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.N))
            {
                if (!medicineNameTextBox.IsEnabled)
                {

                }
                else
                {
                    DoctorAddNewIngredientWindow.GetInstance(_medicineToPreview).ShowDialog();
                }
            }
            else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.D))
            {
                if (!medicineNameTextBox.IsEnabled)
                {

                }
                else
                {
                    if ((MedicineIngredient)medicineIngredientsTable.SelectedItem != null)
                        _medicineToPreview.Ingredients.Remove((MedicineIngredient)medicineIngredientsTable.SelectedItem);
                    RefreshTable();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void medicineTypeComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void medicineReplecmentListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void medicineIngredientsTable_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
