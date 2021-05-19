using HospitalInformationSystem.Controller;
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

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for AddAllergen.xaml
    /// </summary>
    public partial class AddAllergen : Window
    {
        Patient patientToAddAllergen;
        public AddAllergen(Patient patientToAddAllergen)
        {
            InitializeComponent();
            this.patientToAddAllergen = patientToAddAllergen;
        }

        private void addAllergenBtn_Click(object sender, RoutedEventArgs e)
        {
            Allergen newAllergen = new Allergen(allergenNameTxt.Text);
            newAllergen.isAllergic = false;
            PatientController.getInstance().addAllergen(newAllergen);
            PatientController.getInstance().AddAllergenToPatient(patientToAddAllergen, allergenNameTxt.Text);
        }
    }
}
