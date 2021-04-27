using Model;
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

namespace HospitalInformationSystem.Windows.DoctorGUI
{
    /// <summary>
    /// Interaction logic for AnamnesisWindow.xaml
    /// </summary>
    public partial class AnamnesisWindow : Window
    {
        MedicalRecord medicalRecord;
        public AnamnesisWindow(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.medicalRecord = medicalRecord;
        }

        private void addAnamnesis_Click(object sender, RoutedEventArgs e)
        {
            if(checkData())
            {
                medicalRecord.addAnamnesis(new Anamnesis(DateTime.ParseExact(dateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture), basicDescriptionTextBox.Text, anamnesisTextBox.Text));
                MessageBox.Show("Anamneza je uspesno dodata!", "Date", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public Boolean checkData()
        {
            try
            {
                DateTime date = DateTime.ParseExact(dateTextBox.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Nevalidan format za datum!", "Date", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(basicDescriptionTextBox.Text.Length < 1)
            {
                MessageBox.Show("Polje za osnovne informacije anamneze ne može ostati prazno!", "Description", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (anamnesisTextBox.Text.Length < 1)
            {
                MessageBox.Show("Polje za anamnezu ne može ostati prazno!", "AnamnesisText", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
