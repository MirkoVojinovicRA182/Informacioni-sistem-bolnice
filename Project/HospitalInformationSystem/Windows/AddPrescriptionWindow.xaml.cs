using HospitalInformationSystem.Model;
using System;
using System.Collections.Generic;
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

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for AddPrescriptionWindow.xaml
    /// </summary>
    public partial class AddPrescriptionWindow : Window
    {

        private MedicalRecord medicalRecord;
        public AddPrescriptionWindow(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.medicalRecord = medicalRecord;
        }

        private void addPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if(checkData())
            {
                medicalRecord.addPrescription(new Prescription(medicineTextBox.Text, DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), DateTime.ParseExact(endDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), infoTextBox.Text));
                MessageBox.Show("Recept je uspešno izdat.", "Prescription", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public Boolean checkData()
        {
            if(medicineTextBox.Text.Length < 1)
            {
                MessageBox.Show("Morate uneti lek!", "Medicine", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                DateTime date = DateTime.ParseExact(startDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum", "Datum", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                DateTime date = DateTime.ParseExact(endDateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum", "Date", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(infoTextBox.Text.Length < 1)
            {
                MessageBox.Show("Morate uneti potrebne informacije o dozi leka!", "Medicine", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
