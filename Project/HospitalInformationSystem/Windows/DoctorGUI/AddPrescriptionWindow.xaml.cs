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

        private Patient patient;
        public AddPrescriptionWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            medicineComboBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
        }

        private void addPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if(AllInputsCheck())
            {
                Prescription newPrescription = new Prescription((Medicine)medicineComboBox.SelectedItem, DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(endDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), infoTextBox.Text);
                PatientController.getInstance().AddPrescription(patient, newPrescription);
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

        public bool AllInputsCheck()
        {
            if (CheckInputOfMedicineTextBox() && CheckInputOfInfoTextBox() && CheckInputOfStartDateTextBox() && CheckInputOfEndDateTextBox())
                return true;
            return false;
        }
    }
}
