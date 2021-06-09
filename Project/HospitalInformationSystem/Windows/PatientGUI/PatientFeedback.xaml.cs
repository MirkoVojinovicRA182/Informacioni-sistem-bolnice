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

namespace HospitalInformationSystem.Windows.PatientGUI
{
    /// <summary>
    /// Interaction logic for PatientFeedback.xaml
    /// </summary>
    public partial class PatientFeedback : Window
    {
        Patient patient;
        private static PatientFeedback instance = null;
        public static PatientFeedback GetInstance(Patient patient)
        {
            if (instance == null)
                instance = new PatientFeedback(patient);
            return instance;
        }
        private PatientFeedback(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FeedbackController.GetInstance().AddNewFeedback(new Model.Feedback("Pacijent", DateTime.Now, feedTextBox.Text));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
