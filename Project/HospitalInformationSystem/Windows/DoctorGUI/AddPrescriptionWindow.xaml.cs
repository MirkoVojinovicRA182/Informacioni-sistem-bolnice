using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
using Model;
using System;
using System.Windows;
using System.Windows.Input;
using static HospitalInformationSystem.Utility.Constants;

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
            medicineListBox.ItemsSource = MedicineController.GetInstance().GetAllMedicines();
            startDateTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            endDateTextBox.Text = DateTime.Now.AddDays(7).ToString("dd.MM.yyyy.");
        }
        public bool CheckInputOfMedicineTextBox()
        {
            if (medicineListBox.SelectedIndex < 0)
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
                DateTime startDate = DateTime.ParseExact(startDateTextBox.Text, DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
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
                DateTime endDate = DateTime.ParseExact(endDateTextBox.Text, DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
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
            foreach(MedicineIngredient medicineIngredients in ((Medicine)medicineListBox.SelectedItem).Ingredients)
            {
                if (!CheckPatientsIngredientsAllergens(medicineIngredients))
                {
                    MessageBox.Show("Pacijent je alergican na sastojke leka!", "Alergeni", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return CheckPatientsMedicineAllergens();
        }
        private bool CheckPatientsIngredientsAllergens(MedicineIngredient ingredient)
        {
            foreach (Allergen allergen in _patientToAddPrescription.MedicalRecord.AllergensList)
            {
                if (allergen.Name.Equals(ingredient.Name) && allergen.isAllergic)
                    return false;
            }
            return true;
        }
        private bool CheckPatientsMedicineAllergens()
        {
            foreach (Allergen allergen in _patientToAddPrescription.MedicalRecord.AllergensList)
            {
                if (allergen.Name.Equals(((Medicine)medicineListBox.SelectedItem).Name) && allergen.isAllergic)
                    return false;
            }
            return true;
        }
        public bool AllInputsCheck()
        {
            return (CheckInputOfMedicineTextBox() && CheckInputOfInfoTextBox() && CheckInputOfStartDateTextBox()
                && CheckInputOfEndDateTextBox() && CheckPatientsAllergens());
        }
        private void CheckKeyPress()
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.I))
            {
                if (AllInputsCheck())
                {
                    Prescription newPrescription = new Prescription((Medicine)medicineListBox.SelectedItem, DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(endDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), infoTextBox.Text);
                    PatientController.getInstance().AddPrescription(_patientToAddPrescription, newPrescription);
                    MessageBox.Show("Recept je uspešno izdat.", "Prescription", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void startDateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
        private void endDateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void infoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void medicineListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
