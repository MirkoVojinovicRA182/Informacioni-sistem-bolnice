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

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for ReviewHospitalWindow.xaml
    /// </summary>
    public partial class ReviewHospitalWindow : Window
    {
        private Patient patient;
        public ReviewHospitalWindow(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResetNumberOfFinishedAppointments();
            this.Close();
        }

        private void ResetNumberOfFinishedAppointments()
        {
            patient.Activity.NumberOfFinishedAppointmentsSinceReview = 0;
        }
    }
}
