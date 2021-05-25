using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using System;
using System.Windows;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for AddPrescriptionWindow.xaml
    /// </summary>
    public partial class AddPrescriptionWindow : Window
    {
        private Patient _patientToAddPrescription;
        private static AddPrescriptionWindow instance = null;
        public static AddPrescriptionWindow GetInstance(Patient patientToAddPrescription)
        {
            if (instance == null)
                instance = new AddPrescriptionWindow(patientToAddPrescription);
            return instance;
        }
        private AddPrescriptionWindow(Patient patientToAddPrescription)
        {
            InitializeComponent();
            this._patientToAddPrescription = patientToAddPrescription;
            medicineComboBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
        }
        private void addPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if(AllInputsCheck())
            {
                Prescription newPrescription = new Prescription((Medicine)medicineComboBox.SelectedItem, DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(endDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), infoTextBox.Text);
                PatientController.getInstance().AddPrescription(_patientToAddPrescription, newPrescription);
                MessageBox.Show("Recept je uspešno izdat.", "Prescription", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public bool CheckInputOfMedicineTextBox()
        {
            if (medicineComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Morate uneti lek!", "Medicine", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool CheckInputOfInfoTextBox()
        {
            if (infoTextBox.Text.Length < 1)
            {
                MessageBox.Show("Morate uneti potrebne informacije o dozi leka!", "Medicine", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool CheckInputOfStartDateTextBox()
        {
            try
            {
                DateTime startDate = DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool CheckInputOfEndDateTextBox()
        {
            try
            {
                DateTime endDate = DateTime.ParseExact(endDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum", "Date", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckPatientsAllergens()
        {
            foreach(MedicineIngredient medicineIngredients in ((Medicine)medicineComboBox.SelectedItem).Ingredients)
            {
                if (_patientToAddPrescription.Allergens.Contains(medicineIngredients.Name) || _patientToAddPrescription.Allergens.Contains(((Medicine)medicineComboBox.SelectedItem).Name))
                {
                    MessageBox.Show("Pacijent je alergican na sastojke leka!", "Alergeni", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
        public bool AllInputsCheck()
        {
            return (CheckInputOfMedicineTextBox() && CheckInputOfInfoTextBox() && CheckInputOfStartDateTextBox()
                && CheckInputOfEndDateTextBox() && CheckPatientsAllergens());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
