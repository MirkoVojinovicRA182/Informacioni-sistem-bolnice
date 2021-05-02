using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using System.Collections.Generic;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for MedicineInformationPreview.xaml
    /// </summary>
    public partial class MedicineInformationPreview : Window
    {
        private Medicine medicine;
        public MedicineInformationPreview(Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
            initData();
        }

        private void initData()
        {
            medicineIdLabel.Content = medicine.Id;
            medicineNameTextBox.Text = medicine.Name;
            medicineReplacmentComboBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
            List<TypeOfMedicine> medicineTypeList = new List<TypeOfMedicine>();
            TypeOfMedicine[] typeOfMedicines = { TypeOfMedicine.Dilution, TypeOfMedicine.Pill, TypeOfMedicine.Syrup, TypeOfMedicine.Tablet };
            medicineTypeList.AddRange(typeOfMedicines);
            medicineTypeComboBox.ItemsSource = medicineTypeList;
            medicineTypeComboBox.SelectedItem = medicine.Type;
            medicinePurposeTextBox.Text = medicine.Purpose;
            wayOfUseTextBox.Text = medicine.WayOfUse;
            medicineIngredientsTable.ItemsSource = medicine.Ingredients;
        }

        private void editMedicine_Click(object sender, RoutedEventArgs e)
        {
            if (string.Equals(editMedicine.Content, "Izmeni"))
            {
                medicineNameTextBox.IsEnabled = true;
                medicinePurposeTextBox.IsEnabled = true;
                wayOfUseTextBox.IsEnabled = true;
                medicineReplacmentComboBox.IsEnabled = true;
                medicineTypeComboBox.IsEnabled = true;
                editMedicine.Content = "Zavrsi";
            }
            else
            {
                medicine.Name = medicineNameTextBox.Text;
                medicine.ReplacementMedicine = (Medicine)medicineReplacmentComboBox.SelectedItem;
                medicine.Type = (TypeOfMedicine)medicineTypeComboBox.SelectedItem;
                medicine.Purpose = medicinePurposeTextBox.Text;
                medicine.WayOfUse = wayOfUseTextBox.Text;
            }
            
        }
    }
}
