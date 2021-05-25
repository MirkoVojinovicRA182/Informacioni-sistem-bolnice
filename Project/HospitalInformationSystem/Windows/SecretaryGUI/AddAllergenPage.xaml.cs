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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalInformationSystem.Windows.SecretaryGUI
{
    /// <summary>
    /// Interaction logic for AddAllergenPage.xaml
    /// </summary>
    public partial class AddAllergenPage : Page
    {
        public AddAllergenPage()
        {
            InitializeComponent();
        }
        private void AddAllergenBtn_Click(object sender, RoutedEventArgs e)
        {
            Allergen newAllergen = new Allergen(allergenNameTxt.Text);
            PatientController.getInstance().addAllergen(newAllergen);
            foreach (Patient patient in PatientController.getInstance().getPatient())
            {
                Console.WriteLine(patient);
                //foreach (Allergen a in patient.MedicalRecord.AllergensList)
                //{
                //    if (a.Name == newAllergen.Name)
                //        return;
                //}
                patient.MedicalRecord.AllergensList.Add(new Allergen(newAllergen));
                //Console.WriteLine("DODAO SAM!!!!!!!!    " + patient);
                //Console.WriteLine("Pred foreach alergena od" + patient);
                //foreach (Allergen a in patient.MedicalRecord.AllergensList)
                //{     
                //    Console.WriteLine(a.Name + patient);
                //}

            }

        }
    }
}
