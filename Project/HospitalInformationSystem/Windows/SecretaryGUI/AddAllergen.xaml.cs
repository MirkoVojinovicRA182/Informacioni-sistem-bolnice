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
        public AddAllergen()
        {
            InitializeComponent();
        }

        private void addAllergenBtn_Click(object sender, RoutedEventArgs e)
        {
            Allergen newAllergen = new Allergen(allergenNameTxt.Text);
            newAllergen.isAllergic = false;
            PatientController.getInstance().addAllergen(newAllergen);
        }
    }
}
