using HospitalInformationSystem.Controller;
using HospitalInformationSystem.Model;
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
    /// Interaction logic for DoctorFeedback.xaml
    /// </summary>
    public partial class DoctorFeedback : Window
    {
        Doctor doctor;
        private static DoctorFeedback instance = null;
        public static DoctorFeedback GetInstance(Doctor doctor)
        {
            if (instance == null)
                instance = new DoctorFeedback(doctor);
            return instance;
        }
        private DoctorFeedback(Doctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackController.GetInstance().AddNewFeedback(new Feedback(doctor.Name + " " + doctor.Surname, DateTime.Now, feedbackTextBox.Text));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
