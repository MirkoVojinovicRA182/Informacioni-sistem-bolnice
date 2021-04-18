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

namespace HospitalInformationSystem.Windows
{
    /// <summary>
    /// Interaction logic for ShowPatientInformationWindow.xaml
    /// </summary>
    public partial class ShowPatientInformationWindow : Window
    {

        Patient patient;

        public ShowPatientInformationWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            nameLabel.Content = patient.Name + " " + patient.Surname;
        }
    }
}
