using Model;
using System;
using System.Windows;
using System.Windows.Input;
using static HospitalInformationSystem.Utility.Constants;

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for AnamnesisWindow.xaml
    /// </summary>
    public partial class AnamnesisWindow : Window
    {
        MedicalRecord _patientsMedicalRecord;
        private static AnamnesisWindow instance = null;
        public static AnamnesisWindow GetInstance(MedicalRecord patientsMedicalRecord)
        {
            if (instance == null)
                instance = new AnamnesisWindow(patientsMedicalRecord);
            return instance;
        }
        private AnamnesisWindow(MedicalRecord patientsMedicalRecord)
        {
            InitializeComponent();
            this._patientsMedicalRecord = patientsMedicalRecord;
            dateTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy.");
        }
        private bool CheckDateInput()
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text, DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum!", "Date", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckBasicDescriptionInput()
        {
            if (basicDescriptionTextBox.Text.Length < 1)
            {
                MessageBox.Show("Polje za osnovne informacije anamneze ne može ostati prazno!", "Opis anamneze", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool CheckBasicDescriptionOfAnamnesisInput()
        {
            if (anamnesisTextBox.Text.Length < 1)
            {
                MessageBox.Show("Polje za anamnezu ne može ostati prazno!", "Osnovni opis", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private Boolean CheckAllInputs()
        {
            return CheckBasicDescriptionInput() && CheckBasicDescriptionOfAnamnesisInput() && CheckDateInput();
        }
        private void CheckKeyPress()
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                if (CheckAllInputs())
                {
                    _patientsMedicalRecord.addAnamnesis(new Anamnesis(DateTime.ParseExact(dateTextBox.Text, DATE_TEMPLATE, System.Globalization.CultureInfo.InvariantCulture), basicDescriptionTextBox.Text, anamnesisTextBox.Text));
                    MessageBox.Show("Anamneza je uspesno dodata!", "Dodavanje anamneze", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (Keyboard.IsKeyDown(Key.Escape))
                this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
        private void anamnesisTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void basicDescriptionTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void dateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            CheckKeyPress();
        }
    }
}
